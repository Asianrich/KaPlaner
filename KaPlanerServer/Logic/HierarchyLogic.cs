using KaObjects;
using KaPlanerServer.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace KaPlanerServer.Logic
{
    class StateEintrag
    {

        private readonly ManualResetEvent allDone = new ManualResetEvent(false);
        private int counter = 0;

        public List<HierarchiePackage> child = new List<HierarchiePackage>();
        readonly object _lock = new object();


        /// <summary>
        /// Setzt den Zaehler auf einen gewissen Wert
        /// </summary>
        /// <param name="count">Counter</param>
        public void SetCounter(int count)
        {
            counter = count;
        }

        /// <summary>
        /// Dekrementiert den Zaehler
        /// </summary>
        public void DecrementCounter()
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
        public void Wait()
        {
            allDone.WaitOne();
        }
    }

    public static class HierarchyLogic
    {
        /// <summary>
        /// Settings für HierarchieServer.
        /// </summary>
        /// <param name="isRoot"></param>
        public static void HierarchieSettings(bool isRoot)
        {
            ServerConfig.structure = Structure.HIERARCHY;
            Console.WriteLine("HierarchieSettings");
            if (isRoot)
            {
                ServerConfig.root = ServerConfig.host;
                ServerConfig.serverID = 1;

            }
            else
            {
                string read;
                while (true)
                {
                    Console.WriteLine("Bitte geben sie Adresse von Root ein");
                    read = Console.ReadLine();

                    Package package = new Package
                    {
                        sourceServer = ServerConfig.host.ToString(),

                        hierarchie = new HierarchiePackage()
                    };
                    package.hierarchie.HierarchieRequest = HierarchieRequest.NewServer;

                    if (IPAddress.TryParse(read, out IPAddress address))
                    {
                        package = ServerLogic.Send(package, address);
                    }
                    else
                    {
                        Console.WriteLine("Etwas ist schief gelaufen in der IP-Adresse eingabe");
                    }



                    if (package != null)
                    {
                        Console.WriteLine("Packet ist angekommen");
                        //Logic
                        ServerConfig.root = address;
                        Console.WriteLine("Adresse: " + package.hierarchie.destinationAdress);

                        //Zu wem muss ich mich verbinden? bzw. Registrieren
                        package.hierarchie.HierarchieRequest = HierarchieRequest.RegisterServer;
                        package.hierarchie.sourceAdress = ServerConfig.host.ToString();

                        Console.WriteLine("Verbindung zum Server aufbauen? Y");
                        Console.ReadLine();

                        IPAddress connectServer = IPAddress.Parse(package.hierarchie.destinationAdress);
                        Package receive;
                        while (true)
                        {
                            try
                            {
                                receive = ServerLogic.Send(package, connectServer);
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
                            ServerConfig.serverID = receive.hierarchie.sourceID;
                            ServerLogic.database.newServerEntry(receive.hierarchie.destinationAdress, receive.hierarchie.destinationID);
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
        enum ToDo { Info, Send }
        /// <summary>
        /// Paketauflösung in einem HierarchieNetz.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static HierarchiePackage ResolveHierarchie(HierarchiePackage package)
        {
            int anzConnection = ServerLogic.database.getServerCount();
            string ip = ServerConfig.host.ToString();
            int id = ServerConfig.serverID;

            switch (package.HierarchieRequest)
            {
                case HierarchieRequest.Invite:
                    //Ab hier soll man wissen, an WEN ES GEHEN SOLL UND MUSS!
                    if (package.destinationID != ServerConfig.serverID)
                    {
                        sendHierarchie(getAdress(package.destinationID), ToDo.Send, null);
                    }
                    else
                    {
                        //TO FIX: Schau erstmal nach ob der User existiert
                        if (ServerLogic.database.UserExist(package.login))
                        {
                            ServerLogic.database.SaveInvites(package.login, package.invite);
                        }
                        else
                        {
                            // TO FIX: User existiert nicht?
                        
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
                        StateEintrag stateEintrag = new StateEintrag();
                        List<HierarchiePackage> child = new List<HierarchiePackage>();
                        stateEintrag.SetCounter(ServerLogic.database.getServerCount());
                        int childcount = 0;
                        for (int i = 0; i < ServerLogic.database.getServerCount(); i++)
                        {
                            Console.WriteLine("Dieser Server hat Kinder");
                            int childID = ServerConfig.serverID * 10 + 1 - i; //Weil HierarchieID's!
                            if (ServerLogic.database.ServerExist(childID))
                            {
                                try
                                {
                                    child.Add(sendHierarchie(getAdress(childID), ToDo.Info, stateEintrag));
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
                    int newId = ServerConfig.serverID * 10;
                    //if (Data.ServerConfig.host == Data.ServerConfig.root)
                    //{
                    //    newId += 10;
                    //}
                    //if (database.getServerCount() == 0)
                    //{
                    //    newId += 1;
                    //}
                    if (!ServerLogic.database.ServerExist(newId + 1))
                    {
                        newId += 1;
                    }


                    package.sourceID = newId;
                    ServerLogic.database.newServerEntry(package.sourceAdress, newId);


                    break;
                case HierarchieRequest.RegisterUser:
                    int anzUser = ServerLogic.database.getUserCount();
                    //überprüfen ob USERNAME schon existiert!
                    if (ServerLogic.database.UserExist(package.login))
                    {
                        package.HierarchieAnswer = HierarchieAnswer.UserExistent;
                        break;
                    }
                    else if (anzConnection > 0)
                    {
                        //Eigene Send funktion machen
                        //Soll parallel ablaufen
                        //sendHierarchie();
                        StateEintrag stateEintrag = new StateEintrag();
                        List<HierarchiePackage> child = new List<HierarchiePackage>();
                        stateEintrag.SetCounter(ServerLogic.database.getServerCount());
                        for (int i = 0; i < 2; i++)
                        {
                            int childID = (ServerConfig.serverID * 10) + 1 - i; //Weil HierarchieID's!
                            if (ServerLogic.database.ServerExist(childID))
                            {
                                child.Add(sendHierarchie(getAdress(childID), ToDo.Info, stateEintrag));
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
                    package.HierarchieAnswer = HierarchieAnswer.Success;
                    break;

                case HierarchieRequest.UserLogin:
                    if (package.destinationID == id)
                    {
                        if (ServerLogic.database.UserExist(package.login))
                        {
                            package.destinationAdress = ServerConfig.host.ToString();
                            package.HierarchieAnswer = HierarchieAnswer.Success;
                        }
                        else
                        {
                            package.HierarchieAnswer = HierarchieAnswer.Failure;
                        }
                    }
                    else
                    {
                        package = sendHierarchie(getAdress(package.destinationID), ToDo.Send, null);
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

                int addressid = id;
                if (isUp)
                {
                    addressid /= 10;
                }
                else
                {
                    addressid = child;
                }

                return ServerLogic.database.getServer(addressid);
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

            HierarchiePackage sendHierarchie(string ipadress, ToDo _toDo, StateEintrag state)
            {
                Package hierarchie = new Package
                {
                    hierarchie = package
                };
                //An beide Childs
                if (_toDo == ToDo.Info)
                {
                    hierarchie = ServerLogic.Send(hierarchie, IPAddress.Parse(ipadress));
                    state.DecrementCounter();
                    state.child.Add(hierarchie.hierarchie);
                }
                else if (_toDo == ToDo.Send) //nur an einer gewissen Server
                {
                    hierarchie = ServerLogic.Send(hierarchie, IPAddress.Parse(ipadress));
                }

                return hierarchie.hierarchie;
            }

            return package;
        }
    }
}
