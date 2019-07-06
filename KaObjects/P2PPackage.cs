using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;

namespace KaObjects
{
    public enum P2PRequest { NewServer, Register, Login, Invite}

    [Serializable]
    public class P2PPackage
    {
        static public readonly int TTLinit = 5;
        static public readonly int AnzConnInit = -1; //Unser 'unendlich'. Könnte auch über ein Maximum realisiert werden (denke an RIP).

        public P2PRequest P2Prequest;
        private Guid packageID; //unique ID of this package
        private int ttl = TTLinit; //time to live of this package
        public int anzConn = AnzConnInit; //Vorbelegung mit 'unendlich' oder einem Maximum
        private int anzUser;
        
        //private IPAddress originIPAddress; //this is best an Net.IPAddress so we can check on correct form

        //public IPAddress returnIPAddress;

        public P2PPackage()
        {
            GeneratePID();
            //base.packageReference = this;
        }

        private void GeneratePID()
        {
            packageID = Guid.NewGuid();
        }

        public Guid GetPackageID()
        {
            return packageID;
        }

        public int DecrementTTL()
        {
            return --ttl;
        }

        //public IPAddress GetOriginIPAddress()
        //{
        //    return originIPAddress;
        //}

        //public void SetOriginIPAddress(IPAddress value)
        //{
        //    originIPAddress = value;
        //}
    }
}
