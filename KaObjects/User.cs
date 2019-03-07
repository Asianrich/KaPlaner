using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace KaObjects
{
    [Serializable, XmlRoot("User")]
    public class User
    {

        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public string password { get; set; }





        public User()
        {

        }

        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

    }
}
