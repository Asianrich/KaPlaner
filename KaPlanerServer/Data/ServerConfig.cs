using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace KaPlanerServer.Data
{
    public enum structure {P2P = 0, HIERARCHY }
    public static class ServerConfig
    {
        public static List<IPAddress> ipAddress = new List<IPAddress>();
        private static readonly int LIMIT = 10;
        //HIerarchie
        public static IPAddress root; // = new IPAddress(); <--- richtige Root-Adresse eintragen
        public static int serverID;


        //P2P
        public static List<IPAddress> ListofWellKnown = new List<IPAddress>();
        //dieser Server
        public static IPAddress host;
        private static List<Guid> packageIDs = new List<Guid>();
        public static structure structure = 0;

        static readonly object _object = new object();


        public static bool CheckPackageID(Guid packageID)
        {
            //Ein Thread hintereinander und nicht alle gleichzeitig
            lock (_object)
            {
                if (packageIDs.Exists(x => x == packageID))
                    return false;//Das package wurde bereits bearbeitet.

                packageIDs.Add(packageID);

                if (packageIDs.Count > LIMIT) //Wir brauchen eine Art Limit o.ä. um zu verhindern, dass die Liste übermäßig lang wird.
                    packageIDs.Remove(packageIDs.First());

                return true;//Das package kann bearbeitet werden.
            }
        }



        public static void addServer(IPAddress ip)
        {
            try
            {
                ipAddress.Add(ip);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Server added");
            }
        }


        public static void removeServer(IPAddress ip)
        {
            try
            {
                ipAddress.Remove(ip);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Server removed");
            }
        }
    }
}
