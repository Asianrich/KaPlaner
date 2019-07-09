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
    class stateEintrag
    {
        


    }


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


        private Package send(Package package, IPAddress iPAddress)
        {
            Package receive = new Package();
            ClientConnection client = new ClientConnection(iPAddress);

            //IP-Adressen die er zuschicken soll
            //List<IPAddress> iPAddresses = Data.ServerConfig.ipAddress;

            //Man muss noch überprüfen ob man das Ende ist oder nicht. bzw. wenn TTL 0 ist
            //Wenn ja dann einfach ein null wert zurückgeben
            bool send = true;
            foreach(string visited in package.p2p.visitedPlace)
            {
                if(visited == iPAddress.ToString())
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


            return receive;
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
                package.p2p.visitedPlace.Add(Data.ServerConfig.host.ToString());
                
                if (package.p2p.getTTL() != 0)
                {
                    //Ipadressen rauslesen
                    //packages.Add(send(package,));
                }
                //
                if (packages.Count > 0)
                {
                    package = resolveAll(packages);
                }

            }
            else if (package.hierarchie != null)
            {
                isResolving = resolveHierarchie(package);



            }
            else
            {
                isResolving = true;
            }

            if (isResolving)
            {
                package = resolvePackage(package);
            }


            return package;
        }

        private bool resolveHierarchie(Package package)
        {
            bool isResolve = false;

            //BRAUCHT DAS MICH ZU INTERESSIEREN?!?!?!?!?!?! NUR ERST BEI INVITE!!!!
            //
            if (package.hierarchie.serveradress != Data.ServerConfig.host.ToString())
            {




            }

            switch(package.hierarchie.HierarchieRequest)
            {
                case HierarchieRequest.Invite:
                    //Ab hier soll man wissen, an WEN ES GEHEN SOLL UND MUSS!

                    // Server prueft, ist das Paket fuer rechten Kindserver
                    if(true)
                    {

                    }
                    // Server prueft, ist das Paket fuer linken Kindserver
                    else if (true)
                    {

                    }
                    else
                    {
                        //Fehler im Paket
                    }
                    break;
                case HierarchieRequest.NewServer:

                    //Muss ich weiterleiten an die Unter mir? oder Nicht? entsprechende Antwort schicken
                    break;
                case HierarchieRequest.RegisterServer:

                    break;
                case HierarchieRequest.RegisterUser:
                    break;
                case HierarchieRequest.UserLogin:
                    
                    break;
                default:
                    break;

            }





            return isResolve;
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
            string read;
            bool isP2P = false;
            bool isBoss = false;
            Console.WriteLine("Machen wir P2P(1) oder Hierarchie(2)?");
            while (true)
            {
                read = Console.ReadLine();

                if (read == "1")
                {
                    //TODO Logic
                    isP2P = true;
                    Data.ServerConfig.structure = Data.structure.P2P;
                    break;
                }
                else if (read == "2")
                {
                    Data.ServerConfig.structure = Data.structure.HIERARCHY;
                    //TODO logc? eigentlich net
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
                P2PSettings(isBoss);
            }
            else
            {
                HierarchieSettings(isBoss);
            }

        }

        private void P2PSettings(bool isWellKnown)
        {
            if(isWellKnown)
            {
                Data.ServerConfig.ListofWellKnown.Add(Data.ServerConfig.host);


            }
            else
            {
                string read;
                while (true)
                {
                    Console.WriteLine("Bitte geben sie eine Wellknown-Server an");
                    read = Console.ReadLine();
                    Package package = new Package();
                    package.p2p = new P2PPackage();
                    package.p2p.P2Prequest = P2PRequest.NewServer;
                    package.sourceServer = Data.ServerConfig.host.ToString();


                    package = send(package, IPAddress.Parse(read));


                }
            }




        }

        private void HierarchieSettings(bool isRoot)
        {

            if(isRoot)
            {
                Data.ServerConfig.root = Data.ServerConfig.host;
            }
            else
            {
                string read;
                while (true)
                {
                    Console.WriteLine("Bitte geben sie Adresse von Root ein");
                    read = Console.ReadLine();

                    Package package = new Package();
                    package.sourceServer = Data.ServerConfig.host.ToString();
                    package.hierarchie = new HierarchiePackage();
                    package.hierarchie.HierarchieRequest = HierarchieRequest.NewServer;

                    if (IPAddress.TryParse(read, out IPAddress address))
                    {
                        package = send(package, address);
                    }
                    else
                    {
                        Console.WriteLine("Etwas ist schief gelaufen in der IP-Adresse eingabe");
                    }


                    
                    if(package != null)
                    {
                        //Logic
                        Data.ServerConfig.root = address;


                        //Zu wem muss ich mich verbinden? bzw. Registrieren
                        package.hierarchie.HierarchieRequest = HierarchieRequest.RegisterServer;
                        package.hierarchie.serveradress = Data.ServerConfig.host.ToString();

                        IPAddress connectServer = IPAddress.Parse(package.hierarchie.serveradress);
                        package = send(package, connectServer);
                        Data.ServerConfig.serverID = package.hierarchie.serverID;
                        //Datenbankeintrag
                        if(package != null)
                            database.newServerEntry(package.hierarchie.serveradress, package.hierarchie.destinationID);

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Server nicht verfügbar oder irgendwas ist schief gelaufen");
                    }
                }
            }


        }
        /// <summary>
        /// Resolves and modifies P2PPackages. Returns a list of addresses to forward to.
        /// </summary>
        /// <param name="package"></param>
        /// <returns>List to which further packages are sent.</returns>
        List<IPAddress> IServerLogic.ResolvePackage(P2PPackage package)
        {
            List<IPAddress> returnList = new List<IPAddress>();

            switch (package.P2Prequest)
            {
            //    case P2PRequest.NewServer: //TODO: Es fehlt die Unterscheidung ob es sich um eine Antwort handelt oder nicht. Extra Request? Dann brauchen wir seperate Behandlung der IP Adressen...
            //        //-1. Ist es eine Antwort auf meine Anfrage?
            //        if (package.GetOriginIPAddress() == GetLocalIPAddress()) //oder ipString
            //        {
            //            HandleReturn(package);
            //            break;
            //        }
            //        //0. Gab es die Anfrage schon?
            //        if (!AddPackage(package))
            //            break;
            //        //1. Anzahl Verbindungen (s. neighbours)
            //        if (package.anzConn == P2PPackage.AnzConnInit || package.anzConn >= neighbours.Count)
            //        {//Wenn das Paket noch nicht angefasst wurde, oder wir ein mind. genausogutes Angebot haben geht es als Antwort zurück.
            //            package.anzConn = neighbours.Count;
            //            //2. Antwort zurücksenden (P2PPackage.originIPAddress)
            //            returnList.Add(package.GetOriginIPAddress());
            //        }
            //        package.returnIPAddress = GetLocalIPAddress().ToString();
            //        //3. TTL --
            //        if (package.DecrementTTL() == 0)
            //            break;
            //        //4. Falls TTL > 0 weiterleiten
            //        returnList.AddRange(neighbours); // Flooding
            //        break;
            }

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
