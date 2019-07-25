﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml.Serialization;

namespace KaObjects
{
    public enum HierarchieRequest { NewServer, RegisterServer, RegisterUser, Invite, UserLogin}
    public enum HierarchieAnswer { Success, Failure, Error}
    [Serializable]
    public class HierarchiePackage
    {

        public HierarchieAnswer HierarchieAnswer;
        public HierarchieRequest HierarchieRequest;
        private Guid packageID; //Package ID? braucht man das? just in case
        public KaEvent invite; // Später anschauen
        /// <summary>
        /// ServerID
        /// </summary>
        [XmlElement]
        public int sourceID;

        /// <summary>
        /// ServerQuelle
        /// </summary>
        [XmlElement]
        public string sourceAdress;

        /// <summary>
        /// Destination adress
        /// </summary>
        [XmlElement]
        public string destinationAdress;

        /// <summary>
        /// DestinationID
        /// </summary>
        [XmlElement]
        public int destinationID;

        /// <summary>
        /// Die Anwort auf eine registriation-Anfrage.
        /// </summary>
        [XmlElement]
        public int anzUser = -1;

        /// <summary>
        /// Die Antwort auf einen Server-Verbindungsanfrage
        /// </summary>
        [XmlElement]
        public int anzConnection = -1;

        /// <summary>
        /// Username Existent!!!!!
        /// </summary>
        [XmlElement]
        public string login;

        public HierarchiePackage()
        {
            this.packageID = Guid.NewGuid();
        }

        public Guid GetPackageID()
        {
            return packageID;
        }

    }
}