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
    public class ClientConnection : IClientConnection
    {
        public int port { get => _port; set => _port = value; }

        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private Socket client;
        private byte[] buffer;

        private int _port;



        private void connectServer()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
                client = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
            }
            catch(Exception ex)
            {

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
            catch (Exception ex)
            {
                return;
            }
        }


        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket socket = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesReceive = socket.EndReceive(ar);

                // Signal that all bytes have been sent.  
                receiveDone.Set();
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

        private void Send(Package send)
        {
            byte[] msg = Encoding.ASCII.GetBytes(Serialize(send) + "<EOF>");
            client.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), client);
            sendDone.WaitOne();
        }


        public void Disconnect()
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }



        public string Serialize<T>(T myObject)
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

        public T DeSerialize<T>(string msg)
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

        public Package receive()
        {
            byte[] msg = new byte[8192];
            client.BeginReceive(msg, 0, msg.Length, 0, new AsyncCallback(ReceiveCallback), client);
            receiveDone.WaitOne();
            string[] delimiter = { "<EOF>" };
            string[] recString = Encoding.ASCII.GetString(msg).Split(delimiter,StringSplitOptions.None);


            return DeSerialize<Package>(recString[0]);

        }

        public Package Start(Package state)
        {
            connectServer();
            Package recObject;
            Send(state);

            recObject = receive();
            Disconnect();
            return recObject;
        }
    }
}
