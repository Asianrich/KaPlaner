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
        /// <summary>
        /// Username
        /// </summary>
        [XmlAttribute]
        public string name;
        /// <summary>
        /// Passwort
        /// </summary>
        [XmlAttribute]
        public string password;
        /// <summary>
        /// ServerID Hierarchie
        /// </summary>
        [XmlAttribute]
        public int serverID;

        public User()
        {
            name = "KaPlanerUser"; //Default User
        }

        public User(string name, string password, int serverID)
        {
            this.name = name;
            this.password = password;
            this.serverID = serverID;
        }

        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        /// <summary>
        /// Cheezy
        /// </summary>
        /// <param name="name"></param>
        public User(string name)
        {
            this.name = name;
        }

    }
}
