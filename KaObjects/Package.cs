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
    public enum Request {Test=-1, Failure, Success, Login, Register, Invite}; // Success and Failure are responses from the server

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

    [Serializable, XmlRoot("RegisterPackage")]
    public class RegisterPackage : Package
    {
        public string passwordConfirm;

        public RegisterPackage(User user, string passwordConfirm) : base(Request.Register, user)
        {
            this.passwordConfirm = passwordConfirm;
        }

        public RegisterPackage(Package package, string passwordConfirm) : base(Request.Register, package.user)
        {
            this.passwordConfirm = passwordConfirm;
        }

        public RegisterPackage() { }
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

        public EventPackage() { }

        public KaEvent[] GetKaEvents() { return kaEvents; }
    }
}
