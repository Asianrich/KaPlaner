using KaPlanerServer.Networking;
using System;
using System.IO;

namespace KaPlanerServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "User_Calendar.mdf"));
            Console.WriteLine("|DataDirectory|");

            ServerConnection serverConnection = new ServerConnection();
            serverConnection.Start();
        }
    }
}
