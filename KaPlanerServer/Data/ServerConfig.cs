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


        public static bool getPackageID(Guid packageID)
        {
            //Ein Thread hintereinander und nicht alle gleichzeitig
            lock (_object)
            {
                bool isThere = false;
                for (int i = 0; i < 10; i++)
                {
                    if (packageIDs[i] == packageID)
                    {
                        isThere = true;
                    }
                }


                if (!isThere)
                {
                    if (packageIDs.Count == 10)
                    {
                        //immer das oberste löschen
                        packageIDs.RemoveAt(0);
                    }

                    packageIDs.Add(packageID);
                }

                return isThere;
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

            }
        }
    }
}
