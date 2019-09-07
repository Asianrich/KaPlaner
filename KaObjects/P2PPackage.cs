using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace KaObjects
{
    public enum P2PRequest { NewServer, RegisterServer, NewUser, Login, Invite }
    public enum P2PAnswer { Success, Failure, Error, Visited, Timeout, UserExistent }
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
        /// <summary>
        /// Wird gebraucht um festzustellen ob Anfrage bereits da war.
        /// </summary>
        [XmlElement]
        public Guid packageID; //unique ID of this package
        [XmlElement]
        public int ttl = TTLinit; //time to live of this package
        /// <summary>
        /// Anzahl Connections zu anderen. Anz. Neighbours.
        /// </summary>
        [XmlElement]
        public int anzConn = AnzConnInit; //Vorbelegung mit 'unendlich' oder einem Maximum
        /// <summary>
        /// Anzahl User. Wird gebraucht um festzustellen wer am wenigsten belastet ist.
        /// </summary>
        [XmlElement]
        public int anzUser = AnzUserInit;
        /// <summary>
        /// Name des gesuchten Users. Wird für Invites/Logins gebraucht.
        /// </summary>
        [XmlElement]
        public string Username;
        /// <summary>
        /// Wird für Invites gebraucht.
        /// </summary>
        [XmlElement]
        public KaEvent Invite;
        /// <summary>
        /// Dient der Informationsvermittlung im Fehlerfall
        /// </summary>
        [XmlElement]
        public string ErrorMsg;
        /// <summary>
        /// Weiterleitung von Excpetions
        /// </summary>
        //[XmlElement]
        //public Exception Exception; Eigene Ableitung wird benötigt

        public string GetUsername()
        {
            return Username;
        }

        public KaEvent GetInvite()
        {
            return Invite;
        }

        public P2PPackage()
        {
            this.packageID = Guid.NewGuid();
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
        /// Dies soll immer weitergeleitet werden, dadurch kann man herausfinden wo das Paket durchgelaufen ist
        /// </summary>
        [XmlElement]
        public List<string> visitedPlace = new List<string>();

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
