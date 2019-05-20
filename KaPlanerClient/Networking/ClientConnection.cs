using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using KaObjects;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace KaPlaner.Networking
{
    class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
        Package package = new Package();


    }

    public class ClientConnection : IClientConnection
    {
        public int port { get { return _port; } set { _port = value; } }

        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        private int _port;
        private static string response;


        private Socket connectServer()
        {
            try
            {
                //Lokal Host
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ip = ipHost.AddressList[0];

                //Externer Host
                //IPHostEntry iPHost = Dns.GetHostEntry("192.168.0.3");
                //IPAddress ip = iPHost.AddressList[1];
                IPEndPoint remoteEP = new IPEndPoint(ip, 11000);
                Socket client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                if (!connectDone.WaitOne(10000))
                {
                    throw new Exception("Konnte keine Verbindung aufbauen nach 10 Sekunden");
                }
                return client;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = socket.EndSend(ar);
                ;

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return;
            }
        }


        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket socket = state.workSocket;
                // Complete sending the data to the remote device.  
                int bytesRead = socket.EndReceive(ar);
                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    socket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }


        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }

        //Sending Packages
        private void Send(Socket client, Package send)
        {

            byte[] msg = Encoding.ASCII.GetBytes(Serialize(send) + "<EOF>");
            client.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), client);
        }

        //Successfully disconnecting and closing the Sockets
        private void Disconnect(Socket client)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }



        private string Serialize<T>(T myObject)
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

        private T DeSerialize<T>(string msg)
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


        //Receiving Packages
        private void receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;

                client.BeginReceive(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Starts Connecting, Sending Package, and receiving Response-Package from Server
        /// </summary>
        /// <param name="package">Packages that gets to be sent</param>
        /// <returns></returns>
        public Package Start(Package package)
        {
            Socket client = null;
            try
            {
                client = connectServer();
                Package recObject;
                string[] delimiter = { "<EOF>" };

                Send(client, package);
                sendDone.WaitOne();

                receive(client);
                if (!receiveDone.WaitOne(10000))
                {
                    throw new Exception("Keine Antwort vom Server");
                }

                recObject = DeSerialize<Package>(response.Split(delimiter, StringSplitOptions.None)[0]);
                Disconnect(client);
                return recObject;
            }
            catch (Exception ex)
            {
                if (client != null)
                {
                    Disconnect(client);
                }
                throw ex;
            }
        }
    }
}
