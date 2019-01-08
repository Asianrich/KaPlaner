using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaPlaner.Objects;

namespace KaPlaner.Networking
{
    interface IClientConnection
    {
        User user { get; set; }
        int port { get; set; }

        void connectServer();
        void sendUser();
        void sendEvents();
        User receiveUser();
        KaEvent receiveEvent();







    }
}
