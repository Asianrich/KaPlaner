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
        static readonly string LoginRequest = "Login Requested.";
        static readonly string LoginSuccess = "Login Successful.";
        static readonly string LoginFail = "Login Failed.";
        static readonly string RegisterRequest = "Registry Requested.";
        static readonly string RegisterSuccess = "Registry Successful.";
        static readonly string RegisterFail = "Registry Failed.";
        static readonly string RequestTest = "Test requested.";
        static readonly string RequestUnknown = "Unknown Request.";

        //static readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Yoshi\\source\\repos\\KaPlaner\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";
        static string connectionString = ""Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";

        IDatabase database = new Database(connectionString);
        
        /// <summary>
        /// Resolve Acquired Packages and trigger corresponding requests
        /// </summary>
        /// <param name="package"></param>
        public void resolvePackage(Package package)
        {
            switch (package.request)
            {
                /// In case of Login Request try to login to the server database and set Request accordingly
                case Request.Login:
                    Console.WriteLine(LoginRequest);
                    if (database.login(package.user))
                    {
                        Console.WriteLine(LoginSuccess);
                        package.request = Request.Success;
                    }
                    else
                    {
                        Console.WriteLine(LoginFail);
                        package.request = Request.Failure;
                    }
                    break;
                /// In case of Register Request try to login to the server database and set Request accordingly
                case Request.Register:
                    Console.WriteLine(RegisterRequest);
                    if(package is RegisterPackage)
                        resolveRegisterPackage((RegisterPackage)package);
                    break;

                case Request.Test:
                    Console.WriteLine(RequestTest);
                    break;

                default:
                    Console.WriteLine(RequestUnknown);
                    break;
            }
        }

        void resolveRegisterPackage(RegisterPackage registerPackage)
        {
            if(database.registerUser(registerPackage.user, registerPackage.passwordConfirm))
            {
                Console.WriteLine(RegisterSuccess);
                registerPackage.request = Request.Success;
            }
            else
            {
                Console.WriteLine(RegisterFail);
                registerPackage.request = Request.Failure;
            }
        }
    }
}
