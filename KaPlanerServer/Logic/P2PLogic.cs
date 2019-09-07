using KaObjects;
using KaPlanerServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace KaPlanerServer.Logic
{
    public static class P2PLogic
    {
        static readonly string ErrorOrigin = "Origin of Error: ";

        /// <summary>
        /// Settings für P2PServer. Zugehöriigkeit WellKnownPeers usw.
        /// </summary>
        /// <param name="isWellKnown"></param>
        public static void P2PSettings(bool isWellKnown)
        {
            if (isWellKnown)
            {
                //Data.ServerConfig.ListofWellKnown.Add(Data.ServerConfig.host);
                foreach (IPAddress ipAddress in KnownServers.ListofWellKnownPeers)
                {
                    if (ipAddress.ToString() != ServerConfig.host.ToString())
                    {
                        Package package = new Package
                        {
                            p2p = new P2PPackage()
                        };
                        package.p2p.P2Prequest = P2PRequest.RegisterServer;
                        package.p2p.SetOriginIPAddress(ServerConfig.host.ToString());
                        Package receive = ServerLogic.Send(package, ipAddress);

                        //Nur hinzufügen wenn noch nicht existent
                        if (receive != null && !ServerConfig.neighbours.Exists(x => x.ToString() == ipAddress.ToString()))
                        {
                            ServerConfig.neighbours.Add(ipAddress);
                        }
                    }
                }
            }
            else
            {
                for (int i = 2; i > 0; i--) //'i' represents number of connections to create.
                {
                    Package package = new Package
                    {
                        p2p = new P2PPackage
                        {
                            P2Prequest = P2PRequest.NewServer
                        },
                        sourceServer = ServerConfig.host.ToString()
                    };
                    package.p2p.SetOriginIPAddress(ServerConfig.host.ToString());
                    package = ServerLogic.Send(package, KnownServers.GetRandomWellKnownPeer());

                    if (package != null)
                    {
                        //2 Server mit denen ich mich verbinde und bei denen Registriere
                        if (package.p2p.P2PAnswer == P2PAnswer.Error)
                            Console.WriteLine(package.p2p.ErrorMsg);

                        Package registerPackage = new Package
                        {
                            p2p = new P2PPackage
                            {
                                P2Prequest = P2PRequest.RegisterServer
                            },
                            sourceServer = ServerConfig.host.ToString()
                        };
                        registerPackage.p2p.SetOriginIPAddress(ServerConfig.host.ToString());
                        registerPackage = ServerLogic.Send(registerPackage, IPAddress.Parse(package.p2p.lastIP));
                        if (package != null)
                        {
                            if (registerPackage.p2p.P2PAnswer == P2PAnswer.Success)
                            {
                                ServerConfig.neighbours.Add(IPAddress.Parse(package.p2p.lastIP));
                                Console.WriteLine(ServerLogic.RegisterSuccess);
                            }
                            else
                                Console.WriteLine(ServerLogic.RegisterFail);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Das Handling von P2PPaketen.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static P2PPackage ResolveP2P(P2PPackage package)
        {
            if (ServerConfig.CheckPackageID(package.GetPackageID()))
            {
                try
                {
                    List<P2PPackage> returnList;

                    switch (package.P2Prequest)
                    {
                        case P2PRequest.NewServer:
                            //1. Anzahl Verbindungen (s. neighbours)
                            if (package.anzConn == P2PPackage.AnzConnInit || package.anzConn > ServerConfig.neighbours.Count)
                            {//Wenn das Paket noch nicht angefasst wurde, oder wir ein mind. genausogutes Angebot haben geht es als Antwort zurück.
                                package.anzConn = ServerConfig.neighbours.Count;
                                package.lastIP = ServerConfig.host.ToString();
                            }
                            else if (package.anzConn == ServerConfig.neighbours.Count)
                            {
                                package.lastIP = ServerConfig.host.ToString();
                            }
                            //2. Test on TTL
                            if (package.DecrementTTL() == 0)
                            {
                                package.P2PAnswer = P2PAnswer.Timeout;
                                break;
                            }
                            //3. Forking to other servers via flooding
                            returnList = Forward();
                            //Comparing answers
                            foreach (P2PPackage p in returnList)
                            {
                                if (p.P2PAnswer == P2PAnswer.Error)
                                {
                                    package.ErrorMsg = GenerateErrorString();
                                    /*package.Exception = p.Exception;*/
                                }
                                else if (package.anzConn >= p.anzConn)
                                {
                                    package.anzConn = p.anzConn;
                                    package.lastIP = p.lastIP;
                                }
                            }
                            break;

                        case P2PRequest.RegisterServer:
                            //1. Nimm Server in neighbours auf. (Falls noch nicht existent)
                            if (!ServerConfig.neighbours.Exists(x => x.ToString() == package.GetSource()))
                            {
                                ServerConfig.neighbours.Add(IPAddress.Parse(package.GetSource()));
                                package.P2PAnswer = P2PAnswer.Success;
                            }
                            else
                                package.P2PAnswer = P2PAnswer.Failure;
                            break;

                        case P2PRequest.NewUser:
                            //0. Existiert der User?
                            if (ServerLogic.database.UserExist(package.GetUsername()))
                            {
                                package.P2PAnswer = P2PAnswer.UserExistent;
                                break;
                            }
                            //1. Anzahl User
                            int anzUser = ServerLogic.database.GetUserCount();
                            if (package.anzUser == P2PPackage.AnzUserInit || package.anzUser > anzUser)
                            {// siehe case NewServer
                                package.anzUser = anzUser;
                                package.lastIP = ServerConfig.host.ToString();
                            }
                            else if (package.anzUser == anzUser)
                            {
                                package.lastIP = ServerConfig.host.ToString();
                            }
                            //2. Test on TTL ???? -> Nein! Nicht vereinbar mit nur einem User im gesamten Netz.
                            /*if (package.DecrementTTL() == 0)
                            {
                                package.P2PAnswer = P2PAnswer.Timeout;
                                break;
                            }*/
                            //3. Forking to other servers via flooding
                            returnList = Forward();
                            //Comparing answers
                            foreach (P2PPackage p in returnList)
                            {
                                if (p.P2PAnswer == P2PAnswer.Error)
                                {
                                    package.ErrorMsg = p.ErrorMsg;
                                    /*package.Exception = p.Exception;*/
                                    package.P2PAnswer = P2PAnswer.Error;
                                    return package;
                                }
                                else if (package.anzUser >= p.anzUser)
                                {
                                    package.anzUser = p.anzUser;
                                    package.lastIP = p.lastIP;
                                }
                            }
                            package.P2PAnswer = P2PAnswer.Success;
                            break;
                        /*
                        case P2PRequest.RegisterUser:
                            //Client should connect directly to register.
                            break;
                        */

                        case P2PRequest.Login:
                            //1. Check Datenbank nach user
                            if (!ServerLogic.database.UserExist(package.GetUsername())) //2. Wenn nicht gefunden => weiterleiten
                            {
                                returnList = Forward();
                                //Falls nur ein return => Success
                                if (returnList.Count == 1)
                                {
                                    package.P2PAnswer = returnList.First().P2PAnswer;
                                    package.lastIP = returnList.First().lastIP;
                                }
                                else if (returnList.Count > 1)
                                {
                                    foreach (P2PPackage p in returnList)
                                    {
                                        if (p.P2PAnswer == P2PAnswer.Success)
                                            package.lastIP = p.lastIP;
                                    }
                                }
                                else
                                    package.P2PAnswer = P2PAnswer.Failure; //Error?
                            }
                            else
                            {
                                package.lastIP = ServerConfig.host.ToString();
                                package.P2PAnswer = P2PAnswer.Success;
                            }
                            break;

                        case P2PRequest.Invite:
                            //1. Check Datenbank nach user
                            if (!ServerLogic.database.UserExist(package.GetUsername())) //2. Wenn nicht gefunden => weiterleiten
                            {
                                returnList = Forward();

                                foreach (P2PPackage p in returnList)
                                {
                                    if (p.P2PAnswer == P2PAnswer.Success)
                                    {
                                        package.P2PAnswer = p.P2PAnswer;
                                        package.lastIP = p.lastIP;
                                    }
                                }
                            }
                            else
                            {
                                ServerLogic.database.SaveInvites(package.GetUsername(), package.GetInvite());
                                package.lastIP = ServerConfig.host.ToString();
                                package.P2PAnswer = P2PAnswer.Success;
                            }
                            break;

                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    package.P2PAnswer = P2PAnswer.Error;
                    package.ErrorMsg = GenerateErrorString(ex);
                    /*package.Exception = ex;*/
                    //throw;
                }

            }
            else
            {
                package.P2PAnswer = P2PAnswer.Visited; //Node wurde bereits angefragt, keine Aktion nötig
            }

            return package;

            List<P2PPackage> Forward()
            {
                Package sendPackage = new Package(package);
                Package recievePackage;

                List<P2PPackage> returnList = new List<P2PPackage>();

                foreach (IPAddress iPAddress in ServerConfig.neighbours)
                {
                    recievePackage = ServerLogic.Send(sendPackage, iPAddress);
                    if (recievePackage != null)
                    {
                        if (recievePackage != null && recievePackage.p2p.P2PAnswer == P2PAnswer.Success)
                            return new List<P2PPackage>() { recievePackage.p2p };
                        returnList.Add(recievePackage.p2p);
                    }
                }

                return returnList;
            }
        }

        static string GenerateErrorString()
        {
            return ErrorOrigin + ServerConfig.host.ToString();
        }
        static string GenerateErrorString(Exception exception)
        {
            return ErrorOrigin + ServerConfig.host.ToString() + exception.ToString();
        }
    }
}
