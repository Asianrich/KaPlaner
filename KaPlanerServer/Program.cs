using System;
using System.IO;
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

            Console.WriteLine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "User_Calendar.mdf"));
            Console.WriteLine("|DataDirectory|");

            ServerConnection serverConnection = new ServerConnection();
            serverConnection.start();
        }
    }
}
