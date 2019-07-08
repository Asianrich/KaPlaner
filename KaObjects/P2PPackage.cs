using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;

namespace KaObjects
{
    public enum P2PRequest { NewServer, RegisterServer, RegisterUser, Login, Invite}

    [Serializable]
    public class P2PPackage
    {
        static public readonly int TTLinit = 5;
        static public readonly int AnzConnInit = -1; //Unser 'unendlich'. Könnte auch über ein Maximum realisiert werden (denke an RIP).

        public P2PRequest P2Prequest;
        private Guid packageID; //unique ID of this package
        private int ttl = TTLinit; //time to live of this package
        public int anzConn = AnzConnInit; //Vorbelegung mit 'unendlich' oder einem Maximum
        private int anzUser = -1; // -1 Soll andeuten, das noch keine Aenderungen kam!
        private string server; //
        private string originIPAddress; //has to be string to be able to serialize
        //private IPAddress originIPAddress; //this is best an Net.IPAddress so we can check on correct form
        public string returnIPAddress;
        //public IPAddress returnIPAddress;
        
        public P2PPackage()
        {
            GeneratePID();
            //base.packageReference = this;
        }



        public int getTTL()
        {
            return ttl;
        }


        /// <summary>
        /// Diese Methode soll die Sachen hier abaendern
        /// </summary>
        /// <param name="anzUserServer"></param>
        /// <param name="server"></param>
        public void setAnzUser(int anzUserServer, string server)
        {
            if(anzUser == -1 || anzUser > anzUserServer)
            {
                anzUser = anzUserServer;
                this.server = server;
            }
        }


        private void GeneratePID()
        {
            packageID = Guid.NewGuid();
        }

        public Guid GetPackageID()
        {
            return packageID;
        }

        public int DecrementTTL()
        {
            return --ttl;
        }

        public IPAddress GetOriginIPAddress()
        {
            return IPAddress.Parse(originIPAddress);
        }

        public void SetOriginIPAddress(IPAddress iPAddress)
        {
            originIPAddress = iPAddress.ToString();
        }
    }
}
