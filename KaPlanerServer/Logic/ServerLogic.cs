using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaObjects;
using KaObjects.Storage;
using System.Net;

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
        public static string ipString;


        public string typeofServer = "P2P";


        IDatabase database = new Database(connectionString);

        string IServerLogic.ipString { get => ipString;   set  => ipString = value;   }

        /// <summary>
        /// Resolve Acquired Packages and trigger corresponding requests
        /// </summary>
        /// <param name="package"></param>
        public void resolvePackage(Package package)
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
        }

        private void resolvePackage(P2PPackage package)
        {
            throw new NotImplementedException();
        }

        public Package forwarding(Package package)
        {


            return package;
        }

        private void ipInitialize()
        {
            LinkedList<string> listOfWellKnownPeers = database.GetWellKnownPeers();
            List<string> neighbours = new List<string>
            {
                listOfWellKnownPeers.Find(ipString).Previous.ToString(),
                listOfWellKnownPeers.Find(ipString).Next.ToString()
            };
        }
        /// <summary>
        /// Einstellungen fuer den Server wird abgefragt!!!!
        /// </summary>
        public void Settings()
        {


            string input = "";
            Console.WriteLine("Einstellungen fuer den Server");
            Console.WriteLine("Hierarchie(0) oder P2P(1)?");

            while(input != "0" || input != "1")
            {
                input = Console.ReadLine();
                {

                }
            }
            

            Console.WriteLine("Bin ich Root? J/N");

            while (input != "J" || input != "N")
            {
                input = Console.ReadLine();
                if (input != "J" && input != "N")
                    Console.WriteLine("Bitte J oder N eingeben");
            }

            if(input == "J")
            {
                //WERDE ZUM HOST!
            }
            else
            {
                //WERDE ZUM SKLAVEN!
            }


        }

        /// <summary>
        /// Resolves and modifies P2PPackages. Returns a list of addresses to forward to.
        /// </summary>
        /// <param name="package"></param>
        /// <returns>List to which further packages are sent.</returns>
        List<IPAddress> IServerLogic.resolvePackage(P2PPackage package)
        {
            List<IPAddress> returnList = new List<IPAddress>();

            switch (package.P2Prequest)
            {
                case P2PRequest.NewServer: //TODO: Es fehlt die Unterscheidung ob es sich um eine Antwort handelt oder nicht. Extra Request? Dann brauchen wir seperate Behandlung der IP Adressen...
                    //0. Gab es die Anfrage schon?
                    if (!addPackage(package))
                        break;
                    //1. Anzahl Verbindungen (s. neighbours)
                    if (package.anzConn == P2PPackage.AnzConnInit || package.anzConn >= neighbours.Count)
                    {//Wenn das Packet noch nicht angefasst wurde, oder wir ein mind. genausogutes Angebot haben geht es als Antwort zurück.
                        package.anzConn = neighbours.Count;
                        //2. Antwort zurücksenden (P2PPackage.ipAddress)
                        returnList.Add(package.ipAddress);
                    }
                    //3. TTL --
                    if (package.decrementTTL() == 0)
                        break;
                    //4. Falls TTL > 0 weiterleiten
                    returnList.AddRange(neighbours); // Flooding
                    break;
            }

            return returnList;
        }

        private bool addPackage(P2PPackage package)
        {
            if (recievedPackages.Exists(x => x.getPackageID() == package.getPackageID()))
                return false;

            recievedPackages.Add(package);

            if (recievedPackages.Count > recievedListLimit) //Wir brauchen eine Art Limit o.ä. um zu verhindern, dass die Liste übermäßig lang wird.
                recievedPackages.Remove(recievedPackages.First());

            return true;
        }
    }
}
