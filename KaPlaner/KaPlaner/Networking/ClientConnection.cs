using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Remoting;
using KaPlaner.Objects;



namespace KaPlaner.Networking
{
    class ClientConnection : IClientConnection
    {
        public User user { get => _user; set => _user = value; }
        public int port { get => _port; set => _port = value; }

        public void connectServer()
        {
            throw new NotImplementedException();
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

        private User _user;
        private int _port;





    }
}
