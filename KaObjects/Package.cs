using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace KaObjects
{
    /// <summary>
    /// Enum list of possible Requests
    /// Every  additional Request should be added here
    /// </summary>
    public enum Request { Test = -1, Failure, Success, Login, Register, Invite, Save, Load, Delete }; // Success and Failure are responses from the server


    [Serializable, XmlRoot("Package")]
    [XmlInclude(typeof(P2PPackage))]
    public class Package
    {
        public Request request;
        public User user;

        public P2PPackage p2p;
        //public Package packageReference;
        public HierarchiePackage hierarchie;

        public string passwordConfirm;
        public List<string> Connections = new List<string>();


        //nicht beachten
        public bool isForwarding;

        /// <summary>
        /// Wer schickt das?
        /// </summary>
        public string sourceServer;

        public List<KaEvent> kaEvents;

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
            this.kaEvents = new List<KaEvent>();
            this.kaEvents.Add(kaEvent);
        }

        public Package(Request request, User user, KaEvent kaEvent)
        {
            this.user = user;
            this.request = request;
            this.kaEvents = new List<KaEvent>();
            this.kaEvents.Add(kaEvent);
        }

        public Package() { }
    }
}
