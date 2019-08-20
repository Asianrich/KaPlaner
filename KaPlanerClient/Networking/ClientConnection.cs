using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using KaObjects;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace KaPlaner.Networking
{
    public static class Serial
    {
        public static readonly string Delimiter = "<EOF>";
    }
    
    class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    public class ClientConnection : IClientConnection
    {
        private static readonly ManualResetEvent connectDone = new ManualResetEvent(false);
        private static readonly ManualResetEvent sendDone = new ManualResetEvent(false);
        private static readonly ManualResetEvent receiveDone = new ManualResetEvent(false);

        private static string response;
        IPAddress ip;

        public ClientConnection()
        {
            //Anfangshost oder sonst wer
            ip = IPAddress.Parse("192.168.0.8");
        }

        /// <summary>
        /// Diese Methode versucht eine Socketconnection aufzubauen.
        /// </summary>
        /// <returns>Zielsocket</returns>
        private Socket ConnectServer()
        {
            try
            {
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

        /// <summary>
        /// Wie funktioniert das???
        /// </summary>
        /// <param name="ar"></param>
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = socket.EndSend(ar);


                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Probleme beim Senden" + ex.Message);
                StateObject state = (StateObject)ar.AsyncState;
                state.workSocket.Shutdown(SocketShutdown.Both);
                state.workSocket.Close();
                return;
            }
        }

        /// <summary>
        /// Empfange Antwort
        /// </summary>
        /// <param name="ar"></param>
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
                    Console.WriteLine("Lade ich was runter noch?");
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
                Console.WriteLine("Probleme beim Empfangen");
                Console.WriteLine(e.Message);
                StateObject state = (StateObject)ar.AsyncState;
                //state.workSocket.Shutdown(SocketShutdown.Both);
                state.workSocket.Close();
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


        /// <summary>
        /// Sending Packages.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="send"></param>
        private void Send(Socket client, Package send)
        {

            try
            {
                byte[] msg = Encoding.ASCII.GetBytes(Serialize(send) + Serial.Delimiter);
                Console.WriteLine("Vor Begin Send");

                Console.WriteLine(Encoding.ASCII.GetString(msg));
                client.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), client);
                Console.WriteLine("Nach Begin Send");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        //Successfully disconnecting and closing the Sockets
        private void Disconnect(Socket client)
        {

            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }


        /// <summary>
        /// Serialisierung eines Objektes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myObject"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deserialisierung von erhaltenem Packet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Receiving Packages
        /// </summary>
        /// <param name="client"></param>
        private void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject
                {
                    workSocket = client
                };

                client.BeginReceive(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// ändert die Ip-Adresse
        /// </summary>
        /// <param name="ipAddress"></param>
        public void ChangeIP(string ipAddress)
        {
            ip = IPAddress.Parse(ipAddress);
        }

        static readonly object _send = new object();
        /// <summary>
        /// Starts Connecting, Sending Package, and receiving Response-Package from Server
        /// </summary>
        /// <param name="package">Packages that gets to be sent</param>
        /// <returns></returns>
        public Package Start(Package package)
        {
            lock (_send)
            {
                Socket client = null;
                connectDone.Reset();
                receiveDone.Reset();
                sendDone.Reset();
                try
                {
                    client = ConnectServer();
                    Package recObject;
                    string[] delimiter = { Serial.Delimiter };
                    Console.WriteLine("Methodenaufruf Send");
                    Send(client, package);
                    sendDone.WaitOne();

                    Receive(client);
                    Console.WriteLine("Waiting");
                    if (!receiveDone.WaitOne())
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
                        //Hier probleme?
                        client.Close();
                    }
                    throw ex;
                }
            }
        }
    }
}
