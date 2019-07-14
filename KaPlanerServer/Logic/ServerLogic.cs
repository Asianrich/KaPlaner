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
using System.Threading;


namespace KaPlanerServer.Logic
{
    class stateEintrag
    {

        private ManualResetEvent allDone = new ManualResetEvent(false);
        private int counter = 0;

        public List<HierarchiePackage> child = new List<HierarchiePackage>();
        object _lock = new object();




        /// <summary>
        /// Setzt den Zaehler auf einen gewissen Wert
        /// </summary>
        /// <param name="count">Counter</param>
        public void setCounter(int count)
        {
            counter = count;
        }


        /// <summary>
        /// Dekrementiert den Zaehler
        /// </summary>
        public void decrementCounter()
        {
            lock (_lock)
            {
                counter--;
                if (counter == 0)
                {
                    allDone.Set();
                }
            }
        }
        /// <summary>
        /// Warten bis der Counter auf 0 ist
        /// </summary>
        public void wait()
        {
            allDone.WaitOne();
        }


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
        public static string _ipString;

        IDatabase database = new Database(connectionString);

        string IServerLogic.ipString { get => _ipString; set => _ipString = value; }
        //string IServerLogic.ipString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        //P2P Paket handlen
        private P2PPackage resolveP2P(P2PPackage package)
        {
            if (Data.ServerConfig.CheckPackageID(package.GetPackageID()))
            {

                switch (package.P2Prequest)
                {
                    case P2PRequest.NewServer:
                        //1. Anzahl Verbindungen (s. neighbours)
                        if (package.anzConn == P2PPackage.AnzConnInit || package.anzConn >= neighbours.Count)
                        {//Wenn das Paket noch nicht angefasst wurde, oder wir ein mind. genausogutes Angebot haben geht es als Antwort zurück.
                            package.anzConn = neighbours.Count;
                            package.lastIP = Data.ServerConfig.host.ToString();
                        }
                        //2. Forking to other servers via flooding
                        Forward();
                        break;

                    case P2PRequest.RegisterServer:
                        //1. Nimm Server in neighbours auf.
                        //2. Lege einen Datenbankeintrag an.

                        break;

                    case P2PRequest.NewUser:
                        //1. Anzahl User
                        int anzUser = database.getUserCount();
                        if (package.anzUser == P2PPackage.AnzUserInit || package.anzUser >= anzUser)
                        {//2. siehe case NewServer

                        }
                        break;

                    case P2PRequest.RegisterUser:

                        break;

                    case P2PRequest.Login:
                        //1. Check Datenbank nach user
                        //2. Wenn nicht gefunden => weiterleiten
                        break;

                    case P2PRequest.Invite:

                        break;

                    default:

                        break;
                }
            }
            else
            {
                package.P2PAnswer = P2PAnswer.Visited; //Node wurde bereits angefragt, keine Aktion nötig
            }

            

            return package;

            async Task<bool> Forward()
            {
                if (package.DecrementTTL() == 0)
                    return false;


                //await Task.Run(() => send(package, ));
                //Hier gehört der Sendeaufruf hin.
                //Danach muss die Antwort gehandled werden.
                return true;
            }
        }


        private Package send(Package package, IPAddress iPAddress)
        {
            Package receive = new Package();
            ClientConnection client = new ClientConnection(iPAddress);

            //IP-Adressen die er zuschicken soll
            //List<IPAddress> iPAddresses = Data.ServerConfig.ipAddress;

            //Man muss noch überprüfen ob man das Ende ist oder nicht. bzw. wenn TTL 0 ist
            //Wenn ja dann einfach ein null wert zurückgeben
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
                package.p2p.visitedPlace.Add(Data.ServerConfig.host.ToString());

                package.p2p = resolveP2P(package.p2p);

                if (packages.Count > 0)
                {
                    package = resolveAll(packages);
                }

            }
            else if (package.hierarchie != null)
            {
                package.hierarchie = resolveHierarchie(package.hierarchie);
                package.sourceServer = package.hierarchie.destinationAdress;

            }
            else
            {
                package = resolvePackage(package);
            }

            return package;
        }
        enum toDo { Info, Send }
        private HierarchiePackage resolveHierarchie(HierarchiePackage package)
        {
            int anzConnection = database.getServerCount();
            string ip = Data.ServerConfig.host.ToString();
            int id = Data.ServerConfig.serverID;


            //BRAUCHT DAS MICH ZU INTERESSIEREN?!?!?!?!?!?! NUR ERST BEI INVITE!!!!
            //
            if (package.destinationAdress != Data.ServerConfig.host.ToString())
            {




            }

            switch (package.HierarchieRequest)
            {
                case HierarchieRequest.Invite:
                    //Ab hier soll man wissen, an WEN ES GEHEN SOLL UND MUSS!

                    // Server prueft, ist das Paket fuer rechten Kindserver
                    if (neighbours[0].ToString() == package.destinationAdress)
                    {
                        //sendHierarchie(package, neighbours[0]);
                    }
                    // Server prueft, ist das Paket fuer linken Kindserver
                    else if (neighbours[1].ToString() == package.destinationAdress)
                    {
                        //sendHierarchie(package, neighbours[1]);
                    }
                    else
                    {
                        //sendHierarchie(package, neighbours[0]);
                        //sendHierarchie(package, neighbours[1]);
                    }
                    break;

                case HierarchieRequest.NewServer:

                    if (anzConnection > 0)
                    {
                        //Eigene Send funktion machen
                        //Auch parallel
                        //sendHierarchie();
                        Console.WriteLine("Auf der SUche nach einem passenden Server");
                        stateEintrag stateEintrag = new stateEintrag();

                        stateEintrag.setCounter(database.getServerCount());
                        for (int i = 0; i < database.getServerCount(); i++)
                        {
                            Console.WriteLine("Dieser Server hat Kinder");
                            int childID = Data.ServerConfig.serverID * 10 + 1 - i; //Weil HierarchieID's!
                            Task.Run(() => sendHierarchie(getAdress(childID), toDo.Info, stateEintrag));

                        }
                        //wartet auf die anderen. hoffentlich
                        stateEintrag.wait();
                        List<HierarchiePackage> child = new List<HierarchiePackage>();
                        if (child.Count > 0)
                        {
                            foreach (HierarchiePackage c in child)
                            {
                                if (c.anzConnection <= anzConnection)
                                {
                                    anzConnection = c.anzConnection;
                                    ip = c.destinationAdress;
                                    id = c.destinationID;
                                }
                            }
                        }
                    }
                    package.destinationID = id;
                    package.destinationAdress = ip;
                    package.anzConnection = anzConnection;

                    //Muss ich weiterleiten an die Unter mir? oder Nicht? entsprechende Antwort schicken
                    break;
                case HierarchieRequest.RegisterServer:
                    //Sollte immer durch newServer abgefragt werden!
                    Console.WriteLine("Ein neuer Servereintrag. bzw. Child");
                    int newId = Data.ServerConfig.serverID * 10;
                    if (Data.ServerConfig.host == Data.ServerConfig.root)
                    {
                        newId += 10;
                    }
                    if (database.getServerCount() == 0)
                    {
                        newId += 1;
                    }
                    package.sourceID = newId;
                    database.newServerEntry(package.sourceAdress, newId);
                    

                    break;
                case HierarchieRequest.RegisterUser:
                    int anzUser = database.getUserCount();
                    //überprüfen ob USERNAME schon existiert!
                    if (anzConnection > 0)
                    {

                        //Eigene Send funktion machen
                        //Soll parallel ablaufen
                        //sendHierarchie();
                        stateEintrag stateEintrag = new stateEintrag();
                        List<HierarchiePackage> child = new List<HierarchiePackage>();
                        stateEintrag.setCounter(database.getServerCount());
                        for (int i = 0; i < database.getServerCount(); i++)
                        {
                            int childID = Data.ServerConfig.serverID * 10 + 1 - i; //Weil HierarchieID's!
                            child.Add(sendHierarchie(getAdress(childID), toDo.Info, stateEintrag));

                        }
                        //wartet auf die anderen. hoffentlich

                        
                        if (child.Count > 0)
                        {
                            child = stateEintrag.child;
                            foreach (HierarchiePackage c in child)
                            {
                                if (c.anzUser <= anzUser)
                                {
                                    anzUser = c.anzUser;
                                    ip = c.destinationAdress;
                                    id = c.destinationID;
                                }
                            }
                        }
                    }
                    package.destinationID = id;
                    package.destinationAdress = ip;
                    package.anzUser = anzUser;
                    break;
                case HierarchieRequest.UserLogin:
                    if (package.destinationID == id)
                    {
                        if (database.UserExist(package.login))
                        {
                            package.destinationAdress = Data.ServerConfig.host.ToString();
                            package.HierarchieAnswer = HierarchieAnswer.Success;
                        }
                        else
                        {
                            package.HierarchieAnswer = HierarchieAnswer.Failure;
                        }
                    }
                    else
                    {
                        package = sendHierarchie(getAdress(package.destinationID), toDo.Send, null);



                    }



                    break;
                default:
                    break;

            }

            string getAdress(int ask)
            {
                int numberask = GetDigitCount(ask);
                int numberid = GetDigitCount(id);
                bool isUp = false;
                int child = 0;
                if (numberid >= numberask)
                {
                    isUp = true;
                }
                else
                {

                    int dif = (int)(numberask - numberid);
                    int level = (int)(ask / Math.Pow(10, dif));

                    //Bin ich das?
                    if (level != id)
                    {
                        isUp = true;
                    }
                    else
                    {

                        child = (int)(ask / Math.Pow(10, dif - 1));


                    }

                }

                string address = "";
                int addressid = id;
                if (isUp)
                {
                    addressid = addressid / 10;
                }
                else
                {
                    addressid = child;
                }
                address = database.getServer(id);
                return address;
            }

            int GetDigitCount(int number)
            {
                if (number != 0)
                {
                    double baseExp = Math.Log10(Math.Abs(number));
                    return Convert.ToInt32(Math.Floor(baseExp) + 1);
                }
                else { return 1; }
            }

            HierarchiePackage sendHierarchie(string ipadress, toDo _toDo, stateEintrag state)
            {
                Package hierarchie = new Package();
                //An beide Childs
                if (_toDo == toDo.Info)
                {


                    hierarchie = send(hierarchie, IPAddress.Parse(ipadress));
                    state.decrementCounter();
                    state.child.Add(hierarchie.hierarchie);
                }
                else if (_toDo == toDo.Send) //nur an einer gewissen Server
                {
                    hierarchie = send(hierarchie, IPAddress.Parse(ipadress));
                }

                return hierarchie.hierarchie;
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
                    if (package.serverSwitched)
                    {
                        if (database.login(package.user))
                        {
                            List<KaEvent> kaEvents;
                            kaEvents = database.read(package.user.name);
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
                        if (Data.ServerConfig.structure == Data.structure.HIERARCHY)
                        {
                            HierarchiePackage hierarchie = new HierarchiePackage();
                            hierarchie.HierarchieRequest = HierarchieRequest.UserLogin;
                            hierarchie.login = package.user.name;
                            hierarchie.destinationID = package.user.serverID;
                            hierarchie = resolveHierarchie(hierarchie);
                            if (hierarchie.HierarchieAnswer == HierarchieAnswer.Success)
                            {
                                package.sourceServer = hierarchie.destinationAdress;
                                writeResult(Request.changeServer, "ChangeServer");
                            }
                            else
                            {
                                writeResult(Request.Failure, LoginFail);
                            }

                        }
                        else if (Data.ServerConfig.structure == Data.structure.P2P)
                        {

                        }
                        writeResult(Request.changeServer, "ChangeServer");
                    }
                    break;
                /// In case of Register Request try to login to the server database and set Request accordingly
                case Request.Register:
                    Console.WriteLine(RegisterRequest);
                    try
                    {
                        if (package.serverSwitched)
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
                        else
                        {
                            if (Data.ServerConfig.structure == Data.structure.HIERARCHY)
                            {
                                HierarchiePackage hierarchie = new HierarchiePackage();
                                hierarchie.HierarchieRequest = HierarchieRequest.RegisterUser;
                                hierarchie = resolveHierarchie(hierarchie);
                                package.sourceServer = hierarchie.destinationAdress;


                            }
                            else if (Data.ServerConfig.structure == Data.structure.P2P)
                            {

                            }
                            writeResult(Request.changeServer, "ChangeServer");
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
                case Request.Invite:
                    if (Data.ServerConfig.structure == Data.structure.HIERARCHY)
                    {
                        //Bin ich das?
                        if (package.user.serverID == Data.ServerConfig.serverID)
                        {
                            List <User> list = package.kaEvents[0].members;

                            foreach(User member in list)
                            {

                            }


                            //database.SaveInvites(package.kaEvents)



                        }
                        else
                        {
                            //Nope such den server für mich

                        }
                    }


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
            //LinkedList<string> listOfWellKnownPeers = database.GetWellKnownPeers();
            //List<string> neighbours = new List<string>
            //{
            //    listOfWellKnownPeers.Find(_ipString).Previous.ToString(),
            //    listOfWellKnownPeers.Find(_ipString).Next.ToString()
            //};
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
            if (isWellKnown)
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
            Data.ServerConfig.structure = Data.structure.HIERARCHY;
            Console.WriteLine("HierarchieSettings");
            if (isRoot)
            {
                Data.ServerConfig.root = Data.ServerConfig.host;
                Data.ServerConfig.serverID = 1;

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



                    if (package != null)
                    {
                        Console.WriteLine("Packet ist angekommen");
                        //Logic
                        Data.ServerConfig.root = address;
                        Console.WriteLine("Adresse: " + package.hierarchie.destinationAdress);

                        //Zu wem muss ich mich verbinden? bzw. Registrieren
                        package.hierarchie.HierarchieRequest = HierarchieRequest.RegisterServer;
                        package.hierarchie.sourceAdress = Data.ServerConfig.host.ToString();

                        Console.WriteLine("Verbindung zum Root aufbauen? Y");
                        Console.ReadLine();

                        IPAddress connectServer = IPAddress.Parse(package.hierarchie.destinationAdress);
                        Package receive = new Package();
                        while (true)
                        {
                            try
                            {

                                receive = send(package, connectServer);
                                Thread.Sleep(1000);
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Es gab ein Problem. Y fuer nochmal");
                                Console.ReadLine();
                                Console.WriteLine(ex);
                            }
                        }
                        //Datenbankeintrag
                        if (receive != null)
                        {
                            Console.WriteLine("SourceID: " + receive.hierarchie.sourceID);
                            Data.ServerConfig.serverID = receive.hierarchie.sourceID;
                            database.newServerEntry(receive.hierarchie.destinationAdress, receive.hierarchie.destinationID);
                            Console.WriteLine("Adresse: " + receive.hierarchie.destinationAdress);
                            Console.WriteLine("ID: " + receive.hierarchie.destinationID);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Server antwortet nicht richtig, Root nochmal fragen");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Server nicht verfügbar oder irgendwas ist schief gelaufen");
                    }
                }
            }


        }


        private bool AddPackage(P2PPackage package)
        {
            //if (recievedPackages.Exists(x => x.GetPackageID() == package.GetPackageID()))
            //    return false;

            //recievedPackages.Add(package);

            //if (recievedPackages.Count > recievedListLimit) //Wir brauchen eine Art Limit o.ä. um zu verhindern, dass die Liste übermäßig lang wird.
            //    recievedPackages.Remove(recievedPackages.First());

            return true;
        }

        private void HandleReturn(P2PPackage package)
        {
            throw new NotImplementedException();
        }

        List<string> IServerLogic.resolvePackage(P2PPackage package)
        {
            throw new NotImplementedException();
        }
    }
}
