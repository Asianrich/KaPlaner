using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using KaPlaner.Objects;



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




        public void connectServer()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
                client = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(remoteEP,
                new AsyncCallback(ConnectCallback), client);
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
            throw new NotImplementedException();
        }

        public void sendEvents()
        {
            throw new NotImplementedException();
        }

        public void sendUser()
        {
            throw new NotImplementedException();
        }

        public void logging()
        {
            throw new NotImplementedException();
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        private User _user;
        private int _port;





    }
}
