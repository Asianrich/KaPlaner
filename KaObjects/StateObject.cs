using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace KaObjects
{
    [Serializable, XmlRoot("StateObject")]
    public class StateObject
    {
        public string results { get; set; }
        User user;
        KaEvent[] kaEvents;
    }
}
