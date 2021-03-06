﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace KaPlanerServer.Data
{
    public enum Structure { P2P, HIERARCHY }

    public static class ServerConfig
    {
        /// <summary>
        /// connectionString für Datenbank
        /// </summary>
        public static readonly string connectionString = Properties.Settings.Default.ConnectionString;

        /// <summary>
        /// P2P neighbours
        /// </summary>
        public static List<IPAddress> neighbours = new List<IPAddress>();
        private static readonly int LIMIT = 10;

        public static int serverID = 0; // Standardbelegung (0 verweist auf P2P) 

        /// <summary>
        /// Dieser Server
        /// </summary>
        public static IPAddress host;
        private static readonly List<Guid> packageIDs = new List<Guid>();
        public static Structure structure = 0;

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

        /// <summary>
        /// Zufälliger Eintrag aus einer Liste.
        /// Die Liste sollte nicht leer sein.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="EntryList"></param>
        /// <returns>default(T) bei leerer Liste</returns>
        public static T GetRandomEntry<T>(List<T> EntryList)
        {
            if (EntryList.Count == 0) //Check bevor wir falsch indexieren.
                return default;
            Random random = new Random();
            //Random Index zwischen 0 und EntryList.Count-1
            return EntryList[random.Next(EntryList.Count)];
        }
    }
}
