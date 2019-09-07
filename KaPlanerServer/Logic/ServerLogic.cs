using KaObjects;
using KaObjects.Storage;
using KaPlaner.Networking;
using KaPlanerServer.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading;

namespace KaPlanerServer.Logic
{
    class ServerLogic : IServerLogic
    {
        static readonly string LoginRequest = "Login Requested.";
        static readonly string LoginSuccess = "Login Successful.";
        static readonly string LoginFail = "Login Failed.";
        public static readonly string RegisterRequest = "Registry Requested.";
        public static readonly string RegisterSuccess = "Registry Successful.";
        public static readonly string RegisterFail = "Registry Failed.";
        static readonly string UserExistent = "User exists already!";
        static readonly string SaveRequest = "Save Requested.";
        static readonly string SaveSuccess = "Save Success.";
        static readonly string SaveFail = "Save Failed.";
        static readonly string LoadRequest = "Load Requested.";
        static readonly string LoadSuccess = "Load Success.";
        static readonly string LoadFail = "Load Failed.";
        static readonly string InviteRequest = "Invite Requested.";
        static readonly string InviteSuccess = "Invite Success.";
        static readonly string InviteFail = "Invite Failed.";
        static readonly string TestRequest = "Test requested.";
        static readonly string RequestUnknown = "Unknown Request.";
        static readonly string Error = "An Error occurred.";
        static readonly string ChangeServer = "Switching Server...";

        //Connection String ... in VS config auslagern?
        static readonly ConnectionStringSettings AppConnectionString = ConfigurationManager.ConnectionStrings["User_Calendar.mdf"];
        static readonly string connectionString = AppConnectionString.ConnectionString.ToString();

        //ALT: static readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True";
        public static IDatabase database = new Database(connectionString);

        /// <summary>
        /// Einstellungen fuer den Server wird abgefragt!!!!
        /// </summary>
        public void Settings()
        {
            string read;
            bool isP2P = false;
            bool isBoss = false;
            Console.WriteLine("Machen wir P2P(1) oder Hierarchie(2)?");
            while (true)
            {
                read = Console.ReadLine();

                if (read == "1")
                {
                    isP2P = true;
                    ServerConfig.structure = Structure.P2P;
                    break;
                }
                else if (read == "2")
                {
                    ServerConfig.structure = Structure.HIERARCHY;
                    break;
                }
                else
                {
                    Console.WriteLine("Falsche Eingabe");
                }
            }


            Console.WriteLine("Moechten Sie zum Root / P2PNetzwerk verbinden? Y/N");
            while (true)
            {
                read = Console.ReadLine();

                if (read == "Y")
                {
                    break;
                }
                else if (read == "N")
                {
                    isBoss = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Falsche Eingabe");
                }
            }
            if (isP2P)
            {
                P2PLogic.P2PSettings(isBoss);
            }
            else
            {
                HierarchyLogic.HierarchieSettings(isBoss);
            }

        }

        /// <summary>
        /// Auflösen der Paketnachricht.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public Package Resolving(Package package)
        {
            //Muss ich das Paket abaendern oder nicht?
            //bool isResolving = false;
            if (package.p2p != null)
            {
                //P2P Server-Server
                package.p2p.visitedPlace.Add(ServerConfig.host.ToString());
                package.p2p = P2PLogic.ResolveP2P(package.p2p);
            }
            else if (package.hierarchie != null)
            {
                //Hierarchie Server-Server
                package.hierarchie = HierarchyLogic.ResolveHierarchie(package.hierarchie);
                package.sourceServer = package.hierarchie.destinationAdress;
            }
            else
            {
                //CLient-Server
                package = ResolvePackage(package);
            }

            return package;
        }

        /// <summary>
        /// Resolve Acquired Packages and trigger corresponding requests. For Client
        /// </summary>
        /// <param name="package"></param>
        public Package ResolvePackage(Package package)
        {
            switch (package.request)
            {
                /// In case of Login Request try to login to the server database and set Request accordingly
                case Request.Login:
                    Console.WriteLine(LoginRequest);
                    if (package.serverSwitched)
                    {
                        if (database.Login(package.user))
                        {
                            List<KaEvent> kaEvents;
                            kaEvents = database.Read(package.user.name);
                            package.kaEvents = kaEvents;
                            package.invites = database.ReadInvites(package.user.name);
                            writeResult(Request.Success, LoginSuccess);
                        }
                        else
                        {
                            writeResult(Request.Failure, LoginFail);
                        }
                    }
                    else
                    {
                        if (ServerConfig.structure == Structure.HIERARCHY)
                        {
                            ///if serverID > 0 => Hierarchy
                            if (package.user.serverID > 0)
                            {
                                //Hierarchie Login
                                HierarchiePackage hierarchie = new HierarchiePackage
                                {
                                    HierarchieRequest = HierarchieRequest.UserLogin,
                                    login = package.user.name,
                                    destinationID = package.user.serverID
                                };
                                hierarchie = HierarchyLogic.ResolveHierarchie(hierarchie);

                                switch (hierarchie.HierarchieAnswer)
                                {
                                    case HierarchieAnswer.Success:
                                        package.sourceServer = hierarchie.destinationAdress;
                                        writeResult(Request.ChangeServer, ChangeServer);
                                        break;

                                    case HierarchieAnswer.Failure:
                                        writeResult(Request.Failure, LoginFail);
                                        break;

                                    case HierarchieAnswer.Error:
                                        writeResult(Request.Error, Error);
                                        break;

                                    default:
                                        writeResult(Request.Failure, Error);
                                        break;
                                }
                            }
                            ///else => Send to P2P
                            else
                            {
                                //P2P Teil
                                P2PPackage p2p = new P2PPackage(package.user.name)
                                {
                                    P2Prequest = P2PRequest.Login
                                };

                                //Sende Teil
                                Package sendPackage = new Package(p2p);
                                Package recievePackage;
                                recievePackage = Send(sendPackage, KnownServers.GetRandomWellKnownPeer());

                                if (recievePackage != null)
                                {
                                    switch (recievePackage.p2p.P2PAnswer)
                                    {
                                        case P2PAnswer.Success:
                                            package.sourceServer = recievePackage.p2p.lastIP;
                                            writeResult(Request.ChangeServer, ChangeServer);
                                            break;
                                        case P2PAnswer.Failure:
                                            writeResult(Request.Failure, LoginFail);
                                            break;
                                        default:
                                            writeResult(Request.Failure, LoginFail);
                                            break;
                                    }
                                }
                                else
                                {
                                    writeResult(Request.Error, Error);
                                }
                            }
                        }
                        else if (ServerConfig.structure == Structure.P2P)
                        {
                            ///if serverID == 0 => P2P
                            if (package.user.serverID == 0)
                            {
                                //P2P Teil
                                P2PPackage p2p = new P2PPackage(package.user.name)
                                {
                                    P2Prequest = P2PRequest.Login
                                };
                                p2p = P2PLogic.ResolveP2P(p2p);

                                switch (p2p.P2PAnswer)
                                {
                                    case P2PAnswer.Success:
                                        package.sourceServer = p2p.lastIP;
                                        writeResult(Request.ChangeServer, ChangeServer);
                                        break;
                                    case P2PAnswer.Failure:
                                        writeResult(Request.Failure, LoginFail);
                                        break;
                                    default:
                                        writeResult(Request.Failure, LoginFail);
                                        break;
                                }
                            }
                            ///else =>  Send to Hierarchy
                            else
                            {
                                //Hierarchie Login
                                HierarchiePackage hierarchie = new HierarchiePackage
                                {
                                    HierarchieRequest = HierarchieRequest.UserLogin,
                                    login = package.user.name,
                                    destinationID = package.user.serverID
                                };

                                //Sende Teil
                                Package sendPackage = new Package(hierarchie);
                                Package recievePackage;
                                recievePackage = Send(sendPackage, KnownServers.Root);

                                if (recievePackage != null)
                                {
                                    switch (hierarchie.HierarchieAnswer)
                                    {
                                        case HierarchieAnswer.Success:
                                            package.sourceServer = hierarchie.destinationAdress;
                                            writeResult(Request.ChangeServer, ChangeServer);
                                            break;

                                        case HierarchieAnswer.Failure:
                                            writeResult(Request.Failure, LoginFail);
                                            break;

                                        case HierarchieAnswer.Error:
                                            writeResult(Request.Error, Error);
                                            break;

                                        default:
                                            writeResult(Request.Failure, Error);
                                            break;
                                    }
                                }
                                else
                                {
                                    writeResult(Request.Error, Error);
                                }
                            }
                        }
                    }
                    break;

                /// In case of Register Request try to login to the server database and set Request accordingly
                case Request.Register:
                    Console.WriteLine(RegisterRequest);
                    try
                    {
                        if (package.serverSwitched)
                        {
                            if (database.RegisterUser(package.user, package.passwordConfirm))
                            {
                                writeResult(Request.Success, RegisterSuccess);
                                package.user.serverID = ServerConfig.serverID;
                            }
                            else
                            {
                                writeResult(Request.Failure, RegisterFail);
                            }
                        }
                        else
                        {
                            if (ServerConfig.structure == Structure.HIERARCHY)
                            {
                                //Hierarchie Teil
                                HierarchiePackage hierarchie = new HierarchiePackage
                                {
                                    HierarchieRequest = HierarchieRequest.RegisterUser
                                };
                                hierarchie = HierarchyLogic.ResolveHierarchie(hierarchie);

                                if (hierarchie.HierarchieAnswer == HierarchieAnswer.UserExistent)
                                {
                                    writeResult(Request.UserExistent, UserExistent);
                                    break;
                                }

                                //P2P Teil
                                //ConnectP2P(P2PRequest.NewUser, package.user.name, writeResult);

                                //P2P Teil
                                P2PPackage p2p = new P2PPackage(package.user.name)
                                {
                                    P2Prequest = P2PRequest.NewUser
                                };

                                //Sende Teil
                                Package sendPackage = new Package(p2p);
                                Package recievePackage;
                                recievePackage = Send(sendPackage, KnownServers.GetRandomWellKnownPeer());

                                if (recievePackage != null)
                                {
                                    switch (recievePackage.p2p.P2PAnswer)
                                    {
                                        case P2PAnswer.Success:
                                            if (hierarchie.anzUser < recievePackage.p2p.anzUser)
                                                package.sourceServer = hierarchie.destinationAdress;
                                            else
                                                package.sourceServer = recievePackage.p2p.lastIP;
                                            writeResult(Request.ChangeServer, ChangeServer);
                                            break;

                                        ///user already exists
                                        case P2PAnswer.UserExistent:
                                            writeResult(Request.UserExistent, UserExistent);
                                            break;

                                        case P2PAnswer.Error:
                                            Console.WriteLine(recievePackage.p2p.ErrorMsg);
                                            writeResult(Request.Error, Error);
                                            break;

                                        default:
                                            package.sourceServer = hierarchie.destinationAdress;
                                            writeResult(Request.ChangeServer, ChangeServer);
                                            break;
                                    }
                                }
                                else
                                {
                                    package.sourceServer = hierarchie.destinationAdress;
                                    writeResult(Request.ChangeServer, ChangeServer);
                                }
                            }
                            else if (ServerConfig.structure == Structure.P2P)
                            {
                                //P2P Teil
                                P2PPackage p2p = new P2PPackage
                                {
                                    P2Prequest = P2PRequest.NewUser
                                };
                                p2p = P2PLogic.ResolveP2P(p2p);

                                ///user is already existent
                                if (p2p.P2PAnswer == P2PAnswer.Visited)
                                {
                                    writeResult(Request.UserExistent, UserExistent);
                                    break;
                                }

                                //Hierarchie Teil
                                HierarchiePackage hierarchie = new HierarchiePackage
                                {
                                    HierarchieRequest = HierarchieRequest.RegisterUser
                                };

                                //Sende Teil
                                Package sendPackage = new Package(hierarchie);
                                Package recievePackage;
                                recievePackage = Send(sendPackage, KnownServers.Root);
                                if (recievePackage != null)
                                {
                                    switch (recievePackage.hierarchie.HierarchieAnswer)
                                    {
                                        case HierarchieAnswer.Success:
                                            if (p2p.anzUser < recievePackage.hierarchie.anzUser)
                                                package.sourceServer = p2p.lastIP;
                                            else
                                                package.sourceServer = recievePackage.hierarchie.destinationAdress;
                                            writeResult(Request.ChangeServer, RegisterSuccess);
                                            break;
                                        ///user already exists
                                        case HierarchieAnswer.UserExistent:
                                            writeResult(Request.UserExistent, UserExistent);
                                            break;
                                        default:
                                            package.sourceServer = p2p.lastIP;
                                            writeResult(Request.ChangeServer, ChangeServer);
                                            break;
                                    }

                                }
                                else
                                {
                                    package.sourceServer = p2p.lastIP;
                                    writeResult(Request.ChangeServer, ChangeServer);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetType().FullName);
                        Console.WriteLine(e.Message);
                        writeResult(Request.Failure, RegisterFail);
                    }
                    break;

                case Request.Save:
                    Console.WriteLine(SaveRequest);
                    try
                    {
                        database.SaveEvent(package.kaEvents[0]);
                        writeResult(Request.Success, SaveSuccess);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetType().FullName);
                        Console.WriteLine(e.Message);
                        writeResult(Request.Failure, SaveFail);
                    }
                    break;

                case Request.Load:
                    Console.WriteLine(LoadRequest);
                    try
                    {
                        List<KaEvent> kaEvents;
                        //kaEvents = database.LoadEvents(package.user, package.kaEvents[0].Beginn);
                        kaEvents = database.Read(package.user.name);
                        package.kaEvents = kaEvents;
                        writeResult(Request.Success, LoadSuccess);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetType().FullName);
                        Console.WriteLine(e.Message);
                        writeResult(Request.Failure, LoadFail);
                    }
                    break;

                case Request.Test:
                    Console.WriteLine(TestRequest);
                    break;

                case Request.Invite:
                    //Prüfung ServerID
                    Console.WriteLine(InviteRequest);
                    if (ServerConfig.structure == Structure.HIERARCHY)
                    {
                        /*
                         * Hier ist nur Client-Server.
                         * Wenn Server-Server Invite message ist, wird es bei resolveHierarchie erledigt
                         * Das hier sollte wenn möglich als Asynchron, bzw. nach art Fire und Forget. 
                         * Wenn mehrere User eingetragen wird, hat der Server eine lange Anschreibezeit
                         * */

                        List<User> list = package.kaEvents[0].members;

                        foreach (User member in list)
                        {
                            ///member is user in current server
                            if (member.serverID == ServerConfig.serverID)
                            {
                                if (database.UserExist(member.name))
                                {
                                    database.SaveInvites(member.name, package.kaEvents[0]);
                                    writeResult(Request.Success, InviteSuccess);
                                }
                            }
                            ///member is user in hierarchy topology
                            else if (member.serverID > 0)
                            {
                                HierarchiePackage hierarchie = new HierarchiePackage
                                {
                                    HierarchieRequest = HierarchieRequest.Invite,
                                    invite = package.kaEvents[0],
                                    login = member.name,
                                    destinationID = member.serverID
                                };
                                HierarchyLogic.ResolveHierarchie(hierarchie);

                                switch (hierarchie.HierarchieAnswer)
                                {
                                    case HierarchieAnswer.Success:
                                        writeResult(Request.Success, InviteSuccess);
                                        break;
                                    case HierarchieAnswer.Failure:
                                        writeResult(Request.Failure, InviteFail);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            ///member is user in p2p topology
                            else
                            {
                                //P2P Teil
                                P2PPackage p2p = new P2PPackage(member.name, package.kaEvents[0])
                                {
                                    P2Prequest = P2PRequest.Invite
                                };

                                //Sende Teil
                                Package sendPackage = new Package(p2p);
                                Package recievePackage;
                                recievePackage = Send(sendPackage, KnownServers.GetRandomWellKnownPeer());

                                if (recievePackage != null)
                                {
                                    switch (recievePackage.p2p.P2PAnswer)
                                    {
                                        case P2PAnswer.Success:
                                            writeResult(Request.Success, InviteSuccess);
                                            break;

                                        case P2PAnswer.Failure:
                                            writeResult(Request.Failure, InviteFail);
                                            break;

                                        default:
                                            writeResult(Request.Failure, InviteFail);
                                            break;
                                    }
                                }
                                else
                                {
                                    writeResult(Request.Error, Error);
                                }
                            }
                        }
                    }
                    else if (ServerConfig.structure == Structure.P2P)
                    {
                        //Logik P2P Invite
                        List<User> list = package.kaEvents[0].members;

                        foreach (User member in list)
                        {
                            ///member is user in p2p topology
                            if (member.serverID == 0)
                            {
                                if (database.UserExist(member.name))
                                {
                                    database.SaveInvites(member.name, package.kaEvents[0]);
                                    writeResult(Request.Success, InviteSuccess);
                                }
                                else
                                {
                                    P2PPackage p2p = new P2PPackage(member.name, package.kaEvents[0])
                                    {
                                        P2Prequest = P2PRequest.Invite
                                    };
                                    p2p = P2PLogic.ResolveP2P(p2p);

                                    switch (p2p.P2PAnswer)
                                    {
                                        case P2PAnswer.Success:
                                            writeResult(Request.Success, InviteSuccess);
                                            break;

                                        case P2PAnswer.Failure:
                                            writeResult(Request.Failure, InviteFail);
                                            break;

                                        default:
                                            writeResult(Request.Error, Error);
                                            break;
                                    }
                                }
                            }
                            ///member is user in hierarchy topology
                            else
                            {
                                //Hierarchie Teil
                                HierarchiePackage hierarchie = new HierarchiePackage
                                {
                                    HierarchieRequest = HierarchieRequest.Invite,
                                    invite = package.kaEvents[0],
                                    login = member.name,
                                    destinationID = member.serverID
                                };

                                //SendeTeil
                                Package sendPackage = new Package(hierarchie);
                                Package recievePackage;
                                recievePackage = Send(sendPackage, KnownServers.Root);

                                if (recievePackage != null)
                                {
                                    switch (recievePackage.hierarchie.HierarchieAnswer)
                                    {
                                        case HierarchieAnswer.Success:
                                            writeResult(Request.Success, InviteSuccess);
                                            break;

                                        case HierarchieAnswer.Failure:
                                            writeResult(Request.Failure, InviteFail);
                                            break;

                                        default:
                                            writeResult(Request.Failure, InviteFail);
                                            break;
                                    }
                                }
                                else
                                {
                                    writeResult(Request.Error, Error);
                                }
                            }
                        }
                    }
                    break;

                case Request.AnswerInvite:
                    //TODO für P2P und Hierarchisch
                    database.AnswerInvite(package.kaEvents[0], package.user.name, package.answerInvite);
                    break;

                default:
                    Console.WriteLine(RequestUnknown);
                    break;
            }

            return package;

            void writeResult(Request request, string line)
            {
                Console.WriteLine(line);
                package.request = request;
            }
        }

        /// <summary>
        /// Auf null return überprüfen!!!
        /// </summary>
        /// <param name="package"></param>
        /// <param name="iPAddress"></param>
        /// <returns></returns>
        public static Package Send(Package package, IPAddress iPAddress)
        {
            Package receive = new Package();
            ClientConnection client = new ClientConnection(iPAddress);

            //IP-Adressen die er zuschicken soll
            //List<IPAddress> iPAddresses = Data.ServerConfig.ipAddress;

            //Man muss noch überprüfen ob man das Ende ist oder nicht. bzw. wenn TTL 0 ist
            //Wenn ja dann einfach ein null wert zurückgeben
            try
            {
                if (package.p2p != null)
                {
                    bool send = true;
                    foreach (string visited in package.p2p.visitedPlace)
                    {
                        if (visited == iPAddress.ToString())
                        {
                            send = false;
                        }
                    }
                    if (send)
                    {
                        receive = client.Start(package);
                    }
                    else
                    {
                        receive = null;
                    }
                }
                else if (package.hierarchie != null)
                {
                    //Hier Probleme
                    receive = client.Start(package);
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                receive = null;
            }

            return receive;
        }

        /// <summary>
        /// Diese Methode dient zur Anbindung der P2P Topology an die Hierarchische.
        /// Es wird ein entsprechendes Paket in das hierarchische Netz gesendet.
        /// </summary>
        /// <param name="p2pRequest"></param>
        /// <param name="username"></param>
        /// <param name="kaEvent"></param>
        /// <param name="writeResult">übergebe die Methode writeResult</param>
        void ConnectP2P(P2PRequest p2pRequest, string username, KaEvent kaEvent, Action<Request, string> writeResult)
        {
            //P2P Teil
            P2PPackage p2p = new P2PPackage(username, kaEvent)
            {
                P2Prequest = p2pRequest
            };

            //Sende Teil
            Package sendPackage = new Package(p2p);
            Package recievePackage;
            recievePackage = Send(sendPackage, KnownServers.GetRandomWellKnownPeer());

            if (recievePackage != null)
            {
                switch (recievePackage.p2p.P2PAnswer)
                {
                    case P2PAnswer.Success:
                        writeResult(Request.Success, InviteSuccess);
                        break;

                    case P2PAnswer.Failure:
                        writeResult(Request.Failure, InviteFail);
                        break;

                    default:
                        writeResult(Request.Failure, InviteFail);
                        break;
                }
            }
            else
            {
                writeResult(Request.Error, Error);
            }
        }
    }
}
