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
    public enum Request {Test=-1, Login, Register, Invite};

    [Serializable, XmlRoot("Package")]
    public class Package
    {
        public Request request;
        public User user;

        public Package(Request request, User user)
        {
            this.request = request;
            this.user = user;
        }

        public Package() { }
    }

    [Serializable, XmlRoot("EventPackage")] //Does this work?
    public class EventPackage : Package
    {
        KaEvent[] kaEvents;

        public EventPackage(Request request, User user, KaEvent[] kaEvents) : base(request, user)
        {
            this.kaEvents = kaEvents;
        }

        public EventPackage(Package package, KaEvent[] kaEvents) : base(package.request, package.user)
        {
            this.kaEvents = kaEvents;
        }

        public KaEvent[] GetKaEvents() { return kaEvents; }
    }
}
