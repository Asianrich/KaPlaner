using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KaObjects
{
    public enum P2PRequest { NewServer, Register, Login, Invite}

    [Serializable, XmlRoot("P2PPackage")]
    public class P2PPackage : Package
    {
        static public readonly int TTLinit = 5;

        public P2PRequest P2Prequest;
        private Guid packageID; //unique ID of this package
        private int ttl = TTLinit; //time to live of this package
        private int anzConn;
        private int anzUser;
        private string ipAddress;
        public P2PPackage() : base()
        {
            generatePID();
            base.p2p = this;
        }

        private void generatePID()
        {
            packageID = Guid.NewGuid();
        }
    }
}
