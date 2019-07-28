using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;


namespace KaObjects
{
    /// <summary>
    /// Enum list of possible Requests
    /// Every  additional Request should be added here
    /// </summary>
    public enum Request { Test = -1, Error, Failure, Success, Login, Register, Invite, Save, Load, Delete, ChangeServer, AnswerInvite, UserExistent }; // Success and Failure are responses from the server


    [Serializable, XmlRoot("Package")]
    [XmlInclude(typeof(P2PPackage))]//was soll das?
    public class Package
    {
        /// <summary>
        /// Anfrage vom User
        /// </summary>
        [XmlElement]
        public Request request;

        /// <summary>
        /// Es wurde ein Serveranfrage gemacht worden.
        /// </summary>
        public bool serverSwitched;

        /// <summary>
        /// Userdaten, welche uebermittelt wurde
        /// </summary>
        public User user;

        /// <summary>
        /// P2P-Packet. Null wenn nicht P2P.
        /// </summary>
        public P2PPackage p2p;
        /// <summary>
        /// HierarchiePacket. Null wenn nicht Hierarchisch.
        /// </summary>
        public HierarchiePackage hierarchie;

        public string passwordConfirm;
        public bool answerInvite = false;
        /// <summary>
        /// Antwort FUER DEN CLIENT!
        /// </summary>
        public string sourceServer;
        
        public List<KaEvent> kaEvents;
        public List<KaEvent> invites;
        public Package(Request request, User user)
        {
            this.request = request;
            this.user = user;
        }

        /// <summary>
        /// This constructor should only be called to register
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordConfirm"></param>
        public Package(User user, string passwordConfirm)
        {
            this.request = Request.Register; /// This should be the only case for a passwordConfirm
            this.user = user;
            this.passwordConfirm = passwordConfirm;
        }

        public Package(Request request, List<KaEvent> kaEvents)
        {
            this.request = request;
            this.kaEvents = kaEvents;
        }

        public Package(Request request, KaEvent kaEvent)
        {
            this.request = request;
            this.kaEvents = new List<KaEvent>
            {
                kaEvent
            };
        }

        public Package(Request request, User user, KaEvent kaEvent)
        {
            this.user = user;
            this.request = request;
            this.kaEvents = new List<KaEvent>
            {
                kaEvent
            };
        }

        public Package(P2PPackage p2PPackage)
        {
            this.p2p = p2PPackage;
        }

        public Package(HierarchiePackage hierarchiePackage)
        {
            this.hierarchie = hierarchiePackage;
        }

        public Package() { }
    }
}
