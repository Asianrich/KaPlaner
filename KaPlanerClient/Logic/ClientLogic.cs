using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KaObjects.Storage;
using KaPlaner.Networking;
using KaObjects;

namespace KaPlaner.Logic
{
    /// <summary>
    /// Client Logic, die Eingaben der GUI annimmt und überprüft, and die DB weitergibt und
    /// die Verbindung zum Server verwaltet.
    /// </summary>
    public class ClientLogic : IClientLogic
    {
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Yoshi\\source\\repos\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = ""Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";

        IDatabase database = new Database(connectionString);
        User currentUser = new User();

        /// <summary>
        /// Login mit Nutzernamen und Password
        /// Benutzt das Datenbankinterface
        /// TODO: Weitere Kontrollen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool loginLocal(User user)
        {
            currentUser = user;
            return database.login(currentUser);
        }

        /// <summary>
        /// Login mit Nutzernamen und Passwort
        /// Benutzt das Networkinterface
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void loginRemote(User user)
        {
            Package loginPackage = new Package(Request.Login, user);
            IClientConnection clientConnection = new ClientConnection();

            clientConnection.Start(loginPackage);
        }

        /// <summary>
        /// Registrierung eines neuen Benutzers
        /// Benutzt das Datenbankinterface
        /// TODO: Weitere Kontrollen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="password_bestaetigen"></param>
        /// <returns></returns>
        public bool registerLocal(User user, string password_bestaetigen)
        {
            return database.registerUser(user, password_bestaetigen);
        }

        /// <summary>
        /// Registrierung eines Nutzers auf Server
        /// Benutzt das Networkinterface
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="password_bestaetigen"></param>
        /// <returns></returns>
        public void registerRemote(User user, string password_bestaetigen)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Speichern des mitgelieferten Events in der Datenbank
        /// Benutzt das Datenbankinterface
        /// </summary>
        /// <param name="kaEvent"></param>
        public void saveLocal(KaEvent kaEvent)
        {
            database.save(kaEvent);
        }

        /// <summary>
        /// Synchronisieren der Datenbanken
        /// Hier wird das Update durchgeführt
        /// </summary>
        public void syncData()
        {
            throw new NotImplementedException();
        }
    }
}

