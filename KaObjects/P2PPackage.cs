using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;

namespace KaObjects
{
    public enum P2PRequest { NewServer, RegisterServer, NewUser, RegisterUser, Login, Invite }
    public enum P2PAnswer { Success, Failure, Error, Visited, Timeout}
    [Serializable]
    public class P2PPackage
    {
        [XmlElement]
        static public readonly int TTLinit = 5;
        [XmlElement]
        static public readonly int AnzConnInit = -1; //Unser 'unendlich'. Könnte auch über ein Maximum realisiert werden (denke an RIP).
        [XmlElement]
        static public readonly int AnzUserInit = -1; //siehe AnzConnInit
        [XmlElement]
        public P2PRequest P2Prequest;
        [XmlElement]
        public P2PAnswer P2PAnswer = P2PAnswer.Failure;
        [XmlElement]
        public Guid packageID; //unique ID of this package
        [XmlElement]
        public int ttl = TTLinit; //time to live of this package
        [XmlElement]
        public int anzConn = AnzConnInit; //Vorbelegung mit 'unendlich' oder einem Maximum
        [XmlElement]
        public int anzUser = AnzUserInit;
        [XmlElement]
        public readonly string Username;
        [XmlElement]
        public readonly KaEvent Invite;

        public string GetUsername()
        {
            return Username;
        }

        public KaEvent GetInvite()
        {
            return Invite;
        }


        public P2PPackage(string Username) : this()
        {
            this.Username = Username;
        }

        public P2PPackage(string Username, KaEvent Invite) : this(Username)
        {
            this.Invite = Invite;
        }

        /// <summary>
        /// SourceServer
        /// </summary>
        [XmlElement]
        public string source;
        //has to be string to be able to serialize
        //private IPAddress originIPAddress; //this is best an Net.IPAddress so we can check on correct form
        /// <summary>
        /// IP des letzten Knoten, der die niedrigsten Verbindungen aufweist.
        /// </summary>
        [XmlElement]
        public string lastIP;
        /// <summary>
        /// Dies soll immer weitergeleitet werden, dadurch kann man herausfinden wo das Packet durchgelaufen ist
        /// </summary>
        [XmlElement]
        public List<string> visitedPlace = new List<string>();


        /// <summary>
        /// ZielAdresse
        /// </summary>
        //private string destination;

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

        public string GetSource()
        {
            return source;
        }

        public void SetOriginIPAddress(string iPAddress)
        {
            source = iPAddress;
        }



    }
}
