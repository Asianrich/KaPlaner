﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;

namespace KaObjects
{
    public enum P2PRequest { NewServer, RegisterServer, RegisterUser, Login, Invite }

    [Serializable]
    public class P2PPackage
    {
        static public readonly int TTLinit = 5;
        static public readonly int AnzConnInit = -1; //Unser 'unendlich'. Könnte auch über ein Maximum realisiert werden (denke an RIP).

        public P2PRequest P2Prequest;
        private Guid packageID; //unique ID of this package
        private int ttl = TTLinit; //time to live of this package
        public int anzConn = AnzConnInit; //Vorbelegung mit 'unendlich' oder einem Maximum

        /// <summary>
        /// Standard auf -1, => Noch Keine Aenderungen
        /// </summary>
        private int anzUser = -1; 
        /// <summary>
        /// Dies soll immer weitergeleitet werden, dadurch kann man herausfinden wo das Packet durchgelaufen ist
        /// </summary>
        public List<string> visitedPlace = new List<string>();
        /// <summary>
        /// Dies soll als Antwort dienen, heisst wenn NewServer-Anfrage kommt, soll dies als Antwort dienen
        /// </summary>
        private string server; //
        /// <summary>
        /// SourceServer
        /// </summary>
        private string source;
        //has to be string to be able to serialize
        //private IPAddress originIPAddress; //this is best an Net.IPAddress so we can check on correct form

        /// <summary>
        /// ZielAdresse
        /// </summary>
        private string destination;
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
            if (anzUser == -1 || anzUser > anzUserServer)
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

        public string GetSource()
        {
            return source;
        }

        public void SetOriginIPAddress(string iPAddress)
        {
            source = iPAddress;
        }



    }
}
