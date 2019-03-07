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
        public User user { get => _user; set => _user = value; }
        public int port { get => _port; set => _port = value; }




        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private Socket client;
        private byte[] buffer;


        private User _user;
        private int _port;



        public void connectServer()
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

        public KaEvent receiveEvent()
        {
            throw new NotImplementedException();
        }

        public User receiveUser()
        {
            try
            {
                
                client.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveCallback), client);
                receiveDone.WaitOne();

                return null; //User.Deserialize(buffer);


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void sendEvents(KaEvent kaEvent)
        {
            throw new NotImplementedException();
        }

        public void sendUser(User user)
        {
            try
            {

                //SendRq("User", user.Serialize().Length);
                



            }
            catch ( Exception ex)
            {

            }
        }

        public bool logging(User user)
        {
            try
            {
                //byte[] msg = user.Serialize();
                SendRq("Login");


                using (var stream = new NetworkStream(client))
                {




                    XmlDocument myXml = new XmlDocument();
                    


                    XmlSerializer xml = new XmlSerializer(typeof(User));
                    xml.Serialize(stream, user);


                }


                //client.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), client);
                //sendDone.WaitOne();
                int a = 1 + 1;





                //receiveUser();


                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = socket.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

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

        private void SendRq(string request)
        {

            using (var stream = new NetworkStream(client))
            {
                byte[] msg = Encoding.ASCII.GetBytes(String.Format(request)+ '-');
                stream.BeginWrite(msg, 0, msg.Length, new AsyncCallback(SendCallback), client);
                sendDone.WaitOne();
            }

                
            //client.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), client);
            
        }

        





    }
}
