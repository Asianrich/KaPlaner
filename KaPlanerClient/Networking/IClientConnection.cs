using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaPlaner.Objects;

namespace KaPlaner.Networking
{
    public interface IClientConnection
    {
        User user { get; set; }
        int port { get; set; }

        void connectServer();
        bool logging(User user);
        void sendUser(User user);
        void sendEvents(KaEvent kaEvent);
        User receiveUser();
        KaEvent receiveEvent();







    }
}
