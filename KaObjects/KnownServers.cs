using System;
using System.Collections.Generic;
using System.Net;
using KaObjects.Properties;

namespace KaObjects
{
    public static class KnownServers
    {
        //Hierarchie
        public static IPAddress Root = IPAddress.Parse(Settings.Default.Root);

        //P2P
        public static List<IPAddress> ListofWellKnownPeers = new List<IPAddress>()
        {
            IPAddress.Parse(Settings.Default.WellKnownPeer1),
            IPAddress.Parse(Settings.Default.WellKnownPeer2),
            //IPAddress.Parse(Settings.Default.WellKnownPeer3)
        };

        /// <summary>
        /// Gibt zufälligen Well Known Peer zurück.
        /// </summary>
        /// <returns>Zufälliger Well Known Peer</returns>
        public static IPAddress GetRandomWellKnownPeer()
        {
            return GetRandomEntry<IPAddress>(ListofWellKnownPeers);
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
