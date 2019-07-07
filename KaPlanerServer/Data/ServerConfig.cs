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
        public static IPAddress root; // = new IPAddress(); <--- richtige Root-Adresse eintragen
        public static List<IPAddress> ListofWellKnown = new List<IPAddress>();
        public static IPAddress host;
        private static List<Guid> packageIDs = new List<Guid>();
        public static structure structure = 0;

        public static bool getPackageID(Guid packageID)
        {
            bool isThere = false;
            for(int i = 0; i < 10; i++)
            {
                if(packageIDs[i] == packageID)
                {
                    isThere = true;
                }
            }


            if(!isThere)
            {
                if(packageIDs.Count == 10)
                {
                    //immer das oberste löschen
                    packageIDs.RemoveAt(0);
                }

                packageIDs.Add(packageID);
            }

            return isThere;
        }



        public static void addServer(IPAddress ip)
        {
            ipAddress.Add(ip);
        }

        public static void removeServer(IPAddress ip)
        {
            ipAddress.Remove(ip);
        }
    }
}
