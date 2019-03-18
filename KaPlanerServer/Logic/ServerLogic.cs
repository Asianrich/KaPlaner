using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaObjects;
using KaObjects.Storage;

namespace KaPlanerServer.Logic
{
    class ServerLogic : IServerLogic
    {
        static string LoginRequest = "Login Requested.";
        static string LoginSuccess = "Login Successful.";
        static string LoginFail = "Login Failed.";
        static string RequestTest = "Test requested.";
        static string RequestUnknown = "Unknown Request.";

        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Yoshi\\source\\repos\\KaPlaner\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = ""Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";

        IDatabase database = new Database(connectionString);
        
        /// <summary>
        /// Resolve Acquired Packages and trigger corresponding requests
        /// </summary>
        /// <param name="package"></param>
        public void resolvePackage(Package package)
        {
            switch (package.request)
            {
                case Request.Login:
                    Console.WriteLine(LoginRequest);
                    if (database.login(package.user))
                        Console.WriteLine(LoginSuccess);
                    else
                        Console.WriteLine(LoginFail);
                    break;

                case Request.Test:
                    Console.WriteLine(RequestTest);
                    break;

                default:
                    Console.WriteLine(RequestUnknown);
                    break;
            }
        }
    }
}
