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

    [Serializable, XmlRoot("P2PPackage")]
    public class P2PPackage : Package
    {
        static public readonly int TTLinit = 5;
        static public readonly int AnzConnInit = -1; //Unser 'unendlich'. Könnte auch über ein Maximum realisiert werden (denke an RIP).

        public P2PRequest P2Prequest;
        private Guid packageID; //unique ID of this package
        private int ttl = TTLinit; //time to live of this package
        public int anzConn = AnzConnInit; //Vorbelegung mit 'unendlich' oder einem Maximum
        private int anzUser;
        public IPAddress ipAddress; //this is best an Net.IPAddress so we can check on correct form
        public P2PPackage() : base()
        {
            generatePID();
            base.p2p = this;
        }

        private void generatePID()
        {
            packageID = Guid.NewGuid();
        }

        public Guid getPackageID()
        {
            return packageID;
        }

        public int decrementTTL()
        {
            return --ttl;
        }
    }
}
