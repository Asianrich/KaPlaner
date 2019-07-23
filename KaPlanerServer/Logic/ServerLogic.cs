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
        static readonly string InviteRequest = "Invite Requested.";
        static readonly string InviteSuccess = "Invite Success.";
        static readonly string InviteFail = "Invite Failed.";
        static readonly string TestRequest = "Test requested.";
        static readonly string RequestUnknown = "Unknown Request.";

        //Connection String ... in VS config auslagern?
        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True";

        IDatabase database = new Database(connectionString);

        //P2P Paket handlen
        private P2PPackage ResolveP2P(P2PPackage package)
        {
            if (Data.ServerConfig.CheckPackageID(package.GetPackageID()))
            {
                try
                {
                    List<P2PPackage> returnList;

                    switch (package.P2Prequest)
                    {
                        case P2PRequest.NewServer:
                            //1. Anzahl Verbindungen (s. neighbours)
                            if (package.anzConn == P2PPackage.AnzConnInit || package.anzConn > Data.ServerConfig.ipAddress.Count)
                            {//Wenn das Paket noch nicht angefasst wurde, oder wir ein mind. genausogutes Angebot haben geht es als Antwort zurück.
                                package.anzConn = Data.ServerConfig.ipAddress.Count;
                                package.lastIP = Data.ServerConfig.host.ToString();
                            }
                            else if (package.anzConn == Data.ServerConfig.ipAddress.Count)
                            {
                                package.lastIP = Data.ServerConfig.host.ToString();
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
                                if (package.anzConn >= p.anzConn)
                                {
                                    package.anzConn = p.anzConn;
                                    package.lastIP = p.lastIP;
                                }
                            }
                            break;

                        case P2PRequest.RegisterServer:
                            //1. Nimm Server in neighbours auf. (Falls noch nicht existent)
                            if (!Data.ServerConfig.ipAddress.Exists(x => x.ToString() == package.GetSource()))
                                Data.ServerConfig.ipAddress.Add(IPAddress.Parse(package.GetSource()));
                            package.P2PAnswer = P2PAnswer.Success;
                            break;

                        case P2PRequest.NewUser:
                            //1. Anzahl User
                            int anzUser = database.getUserCount();
                            if (package.anzUser == P2PPackage.AnzUserInit || package.anzUser > anzUser)
                            {// siehe case NewServer
                                package.anzUser = anzUser;
                                package.lastIP = Data.ServerConfig.host.ToString();
                            }
                            else if (package.anzUser == anzUser)
                            {
                                package.lastIP = Data.ServerConfig.host.ToString();
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
                                if (package.anzUser >= p.anzUser)
                                {
                                    package.anzUser = p.anzUser;
                                    package.lastIP = p.lastIP;
                                }
                            }
                            break;
                        /*
                        case P2PRequest.RegisterUser:
                            //Client should connect directly to register.
                            break;
                        */

                        case P2PRequest.Login:
                            //1. Check Datenbank nach user
                            if (!database.UserExist(package.GetUsername())) //2. Wenn nicht gefunden => weiterleiten
                            {
                                returnList = Forward();
                                if (returnList.Count != 1 || returnList.First().P2PAnswer != P2PAnswer.Timeout)
                                {
                                    foreach (P2PPackage p in returnList)
                                    {
                                        if (p.P2PAnswer == P2PAnswer.Success)
                                            package.lastIP = p.lastIP;
                                    }
                                }
                            }
                            else
                            {
                                package.lastIP = Data.ServerConfig.host.ToString();
                                package.P2PAnswer = P2PAnswer.Success;
                            }
                            break;

                        case P2PRequest.Invite:
                            //1. Check Datenbank nach user
                            if (!database.UserExist(package.GetUsername())) //2. Wenn nicht gefunden => weiterleiten
                                returnList = Forward();
                            else
                            {
                                database.SaveInvites(package.GetUsername(), package.GetInvite());
                                package.lastIP = Data.ServerConfig.host.ToString();
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

                foreach (IPAddress iPAddress in Data.ServerConfig.ipAddress)
                {
                    recievePackage = Send(sendPackage, iPAddress);
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

        /// <summary>
        /// Auf null return überprüfen!!!
        /// </summary>
        /// <param name="package"></param>
        /// <param name="iPAddress"></param>
        /// <returns></returns>
        private Package Send(Package package, IPAddress iPAddress)
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            //bool isResolving = false;
            if (package.p2p != null)
            {
                //P2P Server-Server
                package.p2p.visitedPlace.Add(Data.ServerConfig.host.ToString());
                package.p2p = ResolveP2P(package.p2p);

                if (packages.Count > 0)
                {
                    package = resolveAll(packages);
                }

            }
            else if (package.hierarchie != null)
            {
                //Hierarchie Server-Server
                package.hierarchie = resolveHierarchie(package.hierarchie);
                package.sourceServer = package.hierarchie.destinationAdress;

            }
            else
            {
                //CLient-Server
                package = ResolvePackage(package);
            }

            return package;
        }
        enum toDo { Info, Send }
        private HierarchiePackage resolveHierarchie(HierarchiePackage package)
        {
            int anzConnection = database.getServerCount();
            string ip = Data.ServerConfig.host.ToString();
            int id = Data.ServerConfig.serverID;

            switch (package.HierarchieRequest)
            {
                case HierarchieRequest.Invite:
                    //Ab hier soll man wissen, an WEN ES GEHEN SOLL UND MUSS!
                    if (package.destinationID != Data.ServerConfig.serverID)
                    {
                        sendHierarchie(getAdress(package.destinationID), toDo.Send, null);
                    }
                    else
                    {
                        //Schau erstmal nach ob der User existiert
                        if (database.UserExist(package.login))
                        {
                            database.SaveInvites(package.login, package.invite);
                        }
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
                        List<HierarchiePackage> child = new List<HierarchiePackage>();
                        stateEintrag.setCounter(database.getServerCount());
                        int childcount = 0;
                        for (int i = 0; i < database.getServerCount(); i++)
                        {
                            Console.WriteLine("Dieser Server hat Kinder");
                            int childID = Data.ServerConfig.serverID * 10 + 1 - i; //Weil HierarchieID's!
                            if (database.ServerExist(childID))
                            {
                                try
                                {
                                    child.Add(sendHierarchie(getAdress(childID), toDo.Info, stateEintrag));
                                    childcount++;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Etwas ist schief gelaufen beim KindServer");
                                }
                            }

                        }
                        //wartet auf die anderen. hoffentlich
                        //stateEintrag.wait();
                        if (childcount != 1) // wenn der Server nur ein Kind hat, so hat er das Sorgerecht für das Kind
                        {
                            //Wenn überhaupt was reingebracht wurde
                            if (child.Count > 0)
                            {
                                foreach (HierarchiePackage c in child)
                                {
                                    //wenn zuverlässigerweise was schief gelaufen ist, wird das ignoriert
                                    if (c != null)
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
                    //if (Data.ServerConfig.host == Data.ServerConfig.root)
                    //{
                    //    newId += 10;
                    //}
                    //if (database.getServerCount() == 0)
                    //{
                    //    newId += 1;
                    //}
                    if (!database.ServerExist(newId + 1))
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
                        for (int i = 0; i < 2; i++)
                        {
                            int childID = (Data.ServerConfig.serverID * 10) + 1 - i; //Weil HierarchieID's!
                            if (database.ServerExist(childID))
                            {
                                child.Add(sendHierarchie(getAdress(childID), toDo.Info, stateEintrag));
                            }

                        }
                        //wartet auf die anderen. hoffentlich


                        if (child.Count > 0)
                        {
                            child = stateEintrag.child;
                            foreach (HierarchiePackage c in child)
                            {
                                if (c != null)
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
                address = database.getServer(addressid);
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
                hierarchie.hierarchie = package;
                //An beide Childs
                if (_toDo == toDo.Info)
                {

                    hierarchie = Send(hierarchie, IPAddress.Parse(ipadress));
                    state.decrementCounter();
                    state.child.Add(hierarchie.hierarchie);
                }
                else if (_toDo == toDo.Send) //nur an einer gewissen Server
                {
                    hierarchie = Send(hierarchie, IPAddress.Parse(ipadress));
                }

                return hierarchie.hierarchie;
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
                            //P2PLogic fürs Login
                            P2PPackage p2p = new P2PPackage(package.user.name)
                            {
                                P2Prequest = P2PRequest.Login
                            };
                            p2p = ResolveP2P(p2p);
                            if (p2p.P2PAnswer == P2PAnswer.Success)
                            {
                                package.sourceServer = p2p.lastIP;
                                writeResult(Request.changeServer, "ChangeServer");
                            }
                            else
                            {
                                writeResult(Request.Failure, LoginFail);
                            }
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
                                package.user.serverID = Data.ServerConfig.serverID;
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
                                //P2P Teil
                                P2PPackage p2p = new P2PPackage();
                                p2p.P2Prequest = P2PRequest.NewUser;
                                p2p = ResolveP2P(p2p);

                                //Hierarchie Teil
                                HierarchiePackage hierarchie = new HierarchiePackage();
                                hierarchie.HierarchieRequest = HierarchieRequest.RegisterUser;

                                //Sende Teil
                                Package sendPackage = new Package(hierarchie);
                                Package recievePackage;
                                recievePackage = Send(sendPackage, Data.ServerConfig.root);
                                if (recievePackage != null)
                                {
                                    if (p2p.anzUser < recievePackage.hierarchie.anzUser)
                                        package.sourceServer = p2p.lastIP;
                                    else
                                        package.sourceServer = recievePackage.hierarchie.destinationAdress;
                                }
                                else
                                {
                                    package.sourceServer = p2p.lastIP;
                                }
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
                    Console.WriteLine(TestRequest);
                    break;
                case Request.Invite:
                    Console.WriteLine(InviteRequest);
                    if (Data.ServerConfig.structure == Data.structure.HIERARCHY)
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
                            if (member.serverID == Data.ServerConfig.serverID)
                            {
                                if (database.UserExist(member.name))
                                    database.SaveInvites(member.name, package.kaEvents[0]);
                            }
                            else
                            {

                                HierarchiePackage hierarchie = new HierarchiePackage();
                                hierarchie.HierarchieRequest = HierarchieRequest.Invite;
                                hierarchie.invite = package.kaEvents[0];
                                hierarchie.login = member.name;
                                hierarchie.destinationID = member.serverID;
                                resolveHierarchie(hierarchie);


                                //User im anderen Server
                            }
                        }
                        //database.SaveInvites(package.kaEvents)
                    }
                    else if (Data.ServerConfig.structure == Data.structure.P2P)
                    {
                        //Logik P2P Invite
                        List<User> list = package.kaEvents[0].members;

                        foreach (User member in list)
                        {
                            if (database.UserExist(member.name))
                            {
                                database.SaveInvites(member.name, package.kaEvents[0]);
                            }
                            else
                            {
                                P2PPackage p2p = new P2PPackage(member.name, package.kaEvents[0]);
                                p2p.P2Prequest = P2PRequest.Invite;
                                p2p = ResolveP2P(p2p);

                                switch (p2p.P2PAnswer)
                                {
                                    case P2PAnswer.Success:
                                        writeResult(Request.Success, InviteSuccess);
                                        break;
                                    case P2PAnswer.Timeout:
                                        writeResult(Request.Success, InviteSuccess);
                                        break;
                                    case P2PAnswer.Failure:
                                        writeResult(Request.Failure, InviteFail);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    break;
                case Request.answerInvite:

                    database.answerInvite(package.kaEvents[0], package.user.name, package.answerInvite);
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
            //Durch Jans Parser, Wellknows rausholen und dann die auf Data.ServerConfig.ListofWellKnown eintragen
            if (isWellKnown)
            {
                //Data.ServerConfig.ListofWellKnown.Add(Data.ServerConfig.host);
                foreach (IPAddress ipAddress in Data.ServerConfig.ListofWellKnown)
                {
                    if (ipAddress.ToString() != Data.ServerConfig.host.ToString())
                    {
                        Package package = new Package();
                        package.p2p = new P2PPackage();
                        package.p2p.P2Prequest = P2PRequest.RegisterServer;
                        package.p2p.SetOriginIPAddress(Data.ServerConfig.host.ToString());
                        Package receive = Send(package, ipAddress);

                        //Nur hinzufügen wenn noch nicht existent
                        if (receive != null && !Data.ServerConfig.ipAddress.Exists(x => x.ToString() == ipAddress.ToString()))
                        {
                            Data.ServerConfig.ipAddress.Add(ipAddress);
                        }
                    }
                }
            }
            else
            {
                for (int i=2; i>0; i--) //'i' represents number of connections to create.
                {
                    Package package = new Package
                    {
                        p2p = new P2PPackage
                        {
                            P2Prequest = P2PRequest.NewServer
                        },
                        sourceServer = Data.ServerConfig.host.ToString()
                    };
                    package.p2p.SetOriginIPAddress(Data.ServerConfig.host.ToString());
                    package = Send(package, Data.ServerConfig.ListofWellKnown[0]);//muss noch randomized werden!!!

                    if (package != null)
                    {
                        //2 Server mit denen ich mich verbinde und bei denen Registriere
                        switch (package.p2p.P2PAnswer)
                        {
                            case P2PAnswer.Success:
                                Package registerPackage = new Package
                                {
                                    p2p = new P2PPackage
                                    {
                                        P2Prequest = P2PRequest.NewServer
                                    },
                                    sourceServer = Data.ServerConfig.host.ToString()
                                };
                                package.p2p.SetOriginIPAddress(Data.ServerConfig.host.ToString());
                                package = Send(registerPackage, IPAddress.Parse(package.p2p.lastIP));
                                if (package != null)
                                {
                                    if (registerPackage.p2p.P2PAnswer == P2PAnswer.Success)
                                    {
                                        Data.ServerConfig.ipAddress.Add(IPAddress.Parse(package.p2p.lastIP));
                                        Console.WriteLine(RegisterSuccess);
                                    }
                                    else
                                        Console.WriteLine(RegisterFail);
                                }
                                break;
                            default:
                                Console.WriteLine("No Success.");
                                break;
                        }
                    }
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
                        package = Send(package, address);
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

                        Console.WriteLine("Verbindung zum Server aufbauen? Y");
                        Console.ReadLine();

                        IPAddress connectServer = IPAddress.Parse(package.hierarchie.destinationAdress);
                        Package receive = new Package();
                        while (true)
                        {
                            try
                            {

                                receive = Send(package, connectServer);
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
    }
}
