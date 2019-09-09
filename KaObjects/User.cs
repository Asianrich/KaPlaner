using System;
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

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else
            {
                User u = (User)obj;
                return u.name == name && u.serverID == serverID;
            }
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() + serverID.GetHashCode();
        }
    }
}
