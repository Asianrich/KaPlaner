﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using KaObjects;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

using KaPlanerServer.Logic;

namespace KaPlanerServer.Networking
{

    //Netztwerkintern. Verschickt die serialisierte Packete

    class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
        public string[] delimiter = { "<EOF>" };
        Package package = new Package();


    }

    class ServerConnection
    {
        static IServerLogic serverLogic = new ServerLogic(); //needs to be static because it is used in a static method

        private IPHostEntry ipHostInfo;
        private IPAddress ipAddress;
        private IPEndPoint localEndPoint;
        private Socket listener;

        public static ManualResetEvent allDone = new ManualResetEvent(false);


        public ServerConnection()
        {
            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //ipAddress = ipHostInfo.AddressList[0]; //4: IP-Adresse 0: fuer Lokal
            ipAddress = IPAddress.Parse(GetAddress(ipHostInfo.AddressList));
            //ipAddress.AddressFamily = AddressFamily.InterNetwork;
            localEndPoint = new IPEndPoint(ipAddress, 11000);
            listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        }

        private string GetAddress(IPAddress[] iPAddresses)
        {
            string adress;
            int result;
            Console.WriteLine("Waehle die IP-Adresse");
            for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
            {
                Console.WriteLine(i + ": " + ipHostInfo.AddressList[i].ToString());
            }
            Console.Write("Ihre Auswahl: ");
            string keyRead = Console.ReadLine();


            while (!Int32.TryParse(keyRead, out result))
            {
                Console.WriteLine("Ein Fehler ist unterlaufen. Versuchen sie erneut");
                keyRead = Console.ReadLine();
            }

            adress = iPAddresses[result].ToString();

            return adress;
        }

        /// <summary>
        /// Aufruf um den Server zu Starten
        /// </summary>
        public void start()
        {
            try
            {
                //Server einstellen. P2P oder Hierarchie inklusive 
                serverLogic.Settings();


                listener.Bind(localEndPoint);
                listener.Listen(100);
                while (true)
                {
                    allDone.Reset();
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            try
            {

                // Signal the main thread to continue.  
                allDone.Set();
                Console.Write("Connection to Client success");
                StateObject state = new StateObject();

                // Get the socket that handles the client request.  
                Socket listener2 = (Socket)ar.AsyncState;
                Socket handler = listener2.EndAccept(ar);

                state.workSocket = handler;

                handler.BeginReceive(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return;
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                Console.WriteLine("Success at closing");
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                string content;
                // Retrieve the socket from the state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;
                // Complete receiving the data from the remote device.  
                int bytesReceive = handler.EndReceive(ar);

                //If there is actually something to read
                if (bytesReceive > 0)
                {

                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesReceive));

                    content = state.sb.ToString();


                    //If the End of File Tag is in there.
                    if (content.IndexOf("<EOF>") > -1)
                    {

                        //string[] msg = content.Split(state.delimiter, StringSplitOptions.None);

                        Package userPackage = DeSerialize<Package>(content.Split(state.delimiter, StringSplitOptions.None)[0]);

                        //Überprüfen des 
                        userPackage = serverLogic.forwarding(userPackage);

                        if (userPackage.isForwarding)
                        {
                            //Lösung
                            communicateServer();


                        }


                        serverLogic.resolvePackage(userPackage);




                        Send(state.workSocket, userPackage);

                    }
                    else
                    {
                        //Not everything was received, trying to receive new Data.
                        handler.BeginReceive(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(ReceiveCallback), state);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                StateObject state = (StateObject)ar.AsyncState;
                state.workSocket.Shutdown(SocketShutdown.Both);
                state.workSocket.Close();
                return;
            }
        }

        //Sending Packages
        public static void Send(Socket handler, Package package)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(Serialize<Package>(package) + "<EOF>");

            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);

        }

        public static void communicateServer()
        {

        }





        //Serializing
        public static string Serialize<T>(T myObject)
        {
            string msg;
            using (var sw = new StringWriter())
            {
                using (var xw = XmlWriter.Create(sw))
                {
                    XmlSerializer xs = new XmlSerializer(myObject.GetType());
                    xs.Serialize(xw, myObject);
                }
                msg = sw.ToString();
            }


            return msg;

        }

        //Deserializing
        public static T DeSerialize<T>(string msg)
        {
            T myObject;
            using (var sr = new StringReader(msg))
            {
                using (var xr = XmlReader.Create(sr))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    myObject = (T)xs.Deserialize(xr);
                }
            }


            return myObject;

        }

    }
}
