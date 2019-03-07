using System;
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


namespace KaPlanerServer.Networking
{
    public class StateObject
    {
        public User user;
        public byte[] buffer;
        public Socket socket;
    }



    class ServerConnection 
    {
        private IPHostEntry ipHostInfo;
        private IPAddress ipAddress;
        private IPEndPoint localEndPoint;
        private Socket listener;

        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static ManualResetEvent receiveDone = new ManualResetEvent(false);
        public static ManualResetEvent sendDone = new ManualResetEvent(false);

        public ServerConnection()
        {
            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            localEndPoint = new IPEndPoint(ipAddress, 11000);
            listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }


        public void start()
        {
            try
            {
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

            }
        }





        public static void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                // Signal the main thread to continue.  
                allDone.Set();
                Console.Write("Someone joined the Hell");

                // Get the socket that handles the client request.  
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);
                StateObject state = new StateObject
                {
                    socket = handler,
                    buffer = new byte[8192]
                };
                string[] delimiter = { ";;;;" };
                string[] msg;
                while (true)
                {
                    


                    //using (var stream = new NetworkStream(handler))
                    //{

                    //    stream.BeginRead(state.buffer, 0, 100, new AsyncCallback(ReceiveCallback),state.socket);
                    //    receiveDone.WaitOne();

                    //}

                    handler.BeginReceive(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(ReceiveCallback), handler);
                    receiveDone.WaitOne();

                    

                    msg = Encoding.ASCII.GetString(state.buffer).Split(delimiter, StringSplitOptions.None);
                    foreach (string satz in msg)
                    {
                        Console.WriteLine(satz);
                    }
                    //state.buffer = new byte[Convert.ToInt32(msg[1])];



                    switch (msg[0])
                    {
                        case "Login":

                            User test;
                            //using (var stream = new NetworkStream(handler))
                            //{

                            //    //using (XmlReader reader = stream)
                            //    //{

                            //    //}
                            //    StreamReader streamReader = new StreamReader(stream);
                            //    string mesg = streamReader.ReadToEnd();



                            //    XmlSerializer serializer = new XmlSerializer(typeof(User));
                            //    //stream.Seek(0, 0);



                            //    var save = (User)serializer.Deserialize(streamReader.BaseStream);
                            //    test = (User)save;



                            //}
                            
                            //handler.BeginReceive(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(ReceiveCallback), state.socket);
                            //receiveDone.WaitOne();
                            string asd = msg[1];
                            Console.WriteLine(asd);
                            User user = DeSerialize<User>(asd);
                            Console.WriteLine(user);
                            Console.WriteLine(user.name);
                            Console.WriteLine(user.password);

                            User sendUser = new User("Jan", "Swathi");

                            //state.buffer = sendUser.Serialize();

                            //handler.BeginSend(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(SendCallback), handler);
                            //sendDone.WaitOne();



                            break;

                        default:
                            break;
                    }





                }


            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return;
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
                Console.WriteLine("Receives : " + bytesReceive);
                // Signal that all bytes have been sent.  
                receiveDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }



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
