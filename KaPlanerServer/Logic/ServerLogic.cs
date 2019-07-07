using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaObjects;
using KaObjects.Storage;
using System.Net;
using System.Net.Sockets;
using KaPlaner.Networking;

namespace KaPlanerServer.Logic
{

    class ServerLogic : IServerLogic
    {
        static readonly string LoginRequest = "Login Requested.";
        static readonly string LoginSuccess = "Login Successful.";
        static readonly string LoginFail = "Login Failed.";
        static readonly string RegisterRequest = "Registry Requested.";
        static readonly string RegisterSuccess = "Registry Successful.";
        static readonly string RegisterFail = "Registry Failed.";
        static readonly string SaveRequest = "Save Requested.";
        static readonly string SaveSuccess = "Save Success.";
        static readonly string SaveFail = "Save Failed.";
        static readonly string LoadRequest = "Load Requested.";
        static readonly string LoadSuccess = "Load Success.";
        static readonly string LoadFail = "Load Failed.";
        static readonly string RequestTest = "Test requested.";
        static readonly string RequestUnknown = "Unknown Request.";

        static readonly int recievedListLimit = 20; //chosen limit for packages to check against

        //Connection String muss noch angepasst werden
        //static readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Yoshi\\source\\repos\\KaPlaner\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\source\\repos\\KaPlaner\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\manhk\\source\\repos\\KaPlaner\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";
        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True";

        private List<IPAddress> neighbours; //Liste der IP Adressen, der Verbindungen (muss min. 2 sein)
        private List<P2PPackage> recievedPackages = new List<P2PPackage>();
        public static string _ipString;


        public string typeofServer = "P2P";


        IDatabase database = new Database(connectionString);

        string IServerLogic.ipString { get => _ipString; set => _ipString = value; }
        //string IServerLogic.ipString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        //P2P Paket handlen
        private bool resolveP2P(P2PPackage package)
        {

            bool isPresent = false;
            isPresent = Data.ServerConfig.getPackageID(package.GetPackageID());

            if (!isPresent)
            {
                switch (package.P2Prequest)
                {
                    case P2PRequest.NewServer: //TODO: Es fehlt die Unterscheidung ob es sich um eine Antwort handelt oder nicht. Extra Request? Dann brauchen wir seperate Behandlung der IP Adressen...
                                               //-1. Ist es eine Antwort auf meine Anfrage?
                                               //if (package.GetOriginIPAddress() == GetLocalIPAddress())
                                               //{
                                               //    HandleReturn(package);
                                               //    break;
                                               //}
                                               ////0. Gab es die Anfrage schon?
                                               //if (!AddPackage(package))
                                               //    break;
                                               ////1. Anzahl Verbindungen (s. neighbours)
                                               //if (package.anzConn == P2PPackage.AnzConnInit || package.anzConn >= neighbours.Count)
                                               //{//Wenn das Paket noch nicht angefasst wurde, oder wir ein mind. genausogutes Angebot haben geht es als Antwort zurück.
                                               //    package.anzConn = neighbours.Count;
                                               //    //2. Antwort zurücksenden (P2PPackage.originIPAddress)
                                               //    returnList.Add(package.GetOriginIPAddress());
                                               //}
                                               //package.returnIPAddress = GetLocalIPAddress();
                                               ////3. TTL --
                                               //if (package.DecrementTTL() == 0)
                                               //    break;
                                               ////4. Falls TTL > 0 weiterleiten
                                               //returnList.AddRange(neighbours); // Flooding
                                               //break;

                    case P2PRequest.RegisterServer:



                        break;
                    case P2PRequest.RegisterUser:
                        int anzUser = database.getUserCount();



                        break;

                    default:
                        break;
                }




            }

            package.DecrementTTL();

            return isPresent;
        }


        public List<Package> send(Package package)
        {
            List<Package> packages = new List<Package>();
            ClientConnection client = new ClientConnection();
            List<IPAddress> iPAddresses = Data.ServerConfig.ipAddress;

            //Man muss noch überprüfen ob man das Ende ist oder nicht. bzw. wenn TTL 0 ist
            //Wenn ja dann einfach ein null wert zurückgeben
            for (int i = 0; i < iPAddresses.Count; i++)
            {
                Package receive = new Package();
                client.changeIP(iPAddresses[i].ToString());
                receive = client.Start(package);

                packages.Add(receive);

            }



            return packages;
        }

        public Package resolveAll(List<Package> packages)
        {
            Package package = new Package();




            return package;
        }

        public Package resolving(Package package)
        {
            List<Package> packages = new List<Package>();
            //Muss ich das Paket abaendern oder nicht?
            bool isResolving = false;
            if (package.p2p != null)
            {
                isResolving = resolveP2P(package.p2p);
                package.visitedPlace.Add(Data.ServerConfig.host.ToString());

                if (package.p2p.getTTL() != 0)
                {
                    packages = send(package);
                }
                //
                if(packages.Count > 0)
                {
                    package = resolveAll(packages);
                }

            }
            else if (package.hierarchie != null)
            {

            }

            if (isResolving)
            {
                package = resolvePackage(package);
            }


            return package;
        }

        /// <summary>
        /// Resolve Acquired Packages and trigger corresponding requests. For Client
        /// </summary>
        /// <param name="package"></param>
        public Package resolvePackage(Package package)
        {

            switch (package.request)
            {
                /// In case of Login Request try to login to the server database and set Request accordingly
                case Request.Login:
                    Console.WriteLine(LoginRequest);
                    if (database.login(package.user))
                    {
                        List<KaEvent> kaEvents;
                        kaEvents = database.read(package.user.name);
                        package.kaEvents = kaEvents;
                        writeResult(Request.Success, LoginSuccess);
                    }
                    else
                    {
                        writeResult(Request.Failure, LoginFail);
                    }
                    break;
                /// In case of Register Request try to login to the server database and set Request accordingly
                case Request.Register:
                    Console.WriteLine(RegisterRequest);
                    try
                    {
                        if (database.registerUser(package.user, package.passwordConfirm))
                        {
                            writeResult(Request.Success, RegisterSuccess);
                        }
                        else
                        {
                            writeResult(Request.Failure, RegisterFail);
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
                        kaEvents = database.read(package.user.name);
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
                    Console.WriteLine(RequestTest);
                    break;

                default:
                    Console.WriteLine(RequestUnknown);
                    break;
            }

            void writeResult(Request request, string line)
            {
                Console.WriteLine(line);
                package.request = request;
            }


            return package;

        }

        public Package resolvePackages(List<Package> packages)
        {
            //Eventuell original mitnehmen?
            Package userPackage = new Package();
            //if(packages[0].p2p != null)
            //{
            //    //resolvePackage()
            //}


            return userPackage;
        }

        private void resolvePackage(P2PPackage package)
        {
            throw new NotImplementedException();
        }

        public Package Forwarding(Package package)
        {


            return package;
        }

        private void ipInitialize()
        {
            LinkedList<string> listOfWellKnownPeers = database.GetWellKnownPeers();
            List<string> neighbours = new List<string>
            {
                listOfWellKnownPeers.Find(_ipString).Previous.ToString(),
                listOfWellKnownPeers.Find(_ipString).Next.ToString()
            };
        }
        /// <summary>
        /// Einstellungen fuer den Server wird abgefragt!!!!
        /// </summary>
        public void Settings()
        {



        }

        /// <summary>
        /// Resolves and modifies P2PPackages. Returns a list of addresses to forward to.
        /// </summary>
        /// <param name="package"></param>
        /// <returns>List to which further packages are sent.</returns>
        List<IPAddress> IServerLogic.ResolvePackage(P2PPackage package)
        {
            List<IPAddress> returnList = new List<IPAddress>();



            return returnList;
        }

        private bool AddPackage(P2PPackage package)
        {
            if (recievedPackages.Exists(x => x.GetPackageID() == package.GetPackageID()))
                return false;

            recievedPackages.Add(package);

            if (recievedPackages.Count > recievedListLimit) //Wir brauchen eine Art Limit o.ä. um zu verhindern, dass die Liste übermäßig lang wird.
                recievedPackages.Remove(recievedPackages.First());

            return true;
        }

        private void HandleReturn(P2PPackage package)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gibt eine geroutete IPAdresse des Hosts zurück.
        /// Handling von mehreren IPAdressen könnte ein Problem sein.
        /// </summary>
        /// <returns></returns>
        private IPAddress GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        List<string> IServerLogic.resolvePackage(P2PPackage package)
        {
            throw new NotImplementedException();
        }
    }
}
