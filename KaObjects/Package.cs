using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace KaObjects
{
    [Serializable, XmlRoot("Package")]
    public class Package
    {
        public string results { get; set; }
        public bool error { get; set; }
        public User user;
        public KaEvent[] kaEvents;
    }
}
