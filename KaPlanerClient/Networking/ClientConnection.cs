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

        public bool logging(User user)
        {
            try
            {
                //byte[] msg = user.Serialize();


                string msg = Serialize(user);

                //byte[] msg = Encoding.ASCII.GetBytes(check);
                SendRq("Login", msg);



                //client.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), client);
                //sendDone.WaitOne();

                int a = 0;
                for (int i = 0; i < 1000; i++)
                {
                    a++;
                }




                //using (var stream = new NetworkStream(client))
                //{




                //    XmlDocument myXml = new XmlDocument();

                //    string msg;
                //    using (var writer = new StringWriter())
                //    {


                //        using (var xmlwriter = XmlWriter.Create(writer))
                //        {

                //            XmlSerializer xml = new XmlSerializer(typeof(User));
                //            xml.Serialize(xmlwriter, user);




                //        }

                //        msg = writer.ToString();


                //    }




                //}



                





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

        private void SendRq(string request, string asd)
        {

            //using (var stream = new NetworkStream(client))
            //{
                
            //    stream.BeginWrite(msg, 0, msg.Length, new AsyncCallback(SendCallback), client);
                
            //}

            byte[] msg = Encoding.ASCII.GetBytes(String.Format(request) + ";;;;" + asd + ";;;;");
            client.BeginSend(msg, 0, msg.Length, 0, new AsyncCallback(SendCallback), client);
            sendDone.WaitOne();
        }


        public void Disconnect()
        {

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

        public T update<T>(User user, KaEvent[] kaEvents)
        {
            throw new NotImplementedException();
        }

        public void disconnect()
        {
            throw new NotImplementedException();
        }

        public StateObject Start(StateObject state)
        {
            connectServer();
            StateObject receive;







            receive = new StateObject();

            return receive;
        }
    }
}
