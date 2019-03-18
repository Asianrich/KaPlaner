using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaPlanerServer.Networking;
using KaPlanerServer.Logic;

namespace KaPlanerServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerConnection serverConnection = new ServerConnection();
            serverConnection.start();
        }
    }
}
