﻿using System;
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
        //static readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Yoshi\\source\\repos\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Richard\\source\\repos\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";

        IDatabase database = new Database(connectionString);
        IClientConnection clientConnection = new ClientConnection();
        User currentUser = new User();

        /// <summary>
        /// Login mit Nutzernamen und Password
        /// Benutzt das Datenbankinterface
        /// TODO: Weitere Kontrollen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LoginLocal(User user)
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
        public bool LoginRemote(User user)
        {
            Package returnPackage;
            Package loginPackage = new Package(Request.Login, user);

            returnPackage = clientConnection.Start(loginPackage);

            return RequestResolve(returnPackage);
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
        public bool RegisterLocal(User user, string password_bestaetigen)
        {
            return database.registerUser(user, password_bestaetigen);
        }

        /// <summary>
        /// Registrierung eines Nutzers auf Server
        /// Benutzt das Networkinterface
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirm"></param>
        /// <returns></returns>
        public bool RegisterRemote(User user, string passwordConfirm)
        {
            Package returnPackage;
            Package registerPackage = new Package(user, passwordConfirm);

            returnPackage = clientConnection.Start(registerPackage);

            return RequestResolve(returnPackage);
        }

        /// <summary>
        /// Speichern des mitgelieferten Events in der Datenbank
        /// Benutzt das Datenbankinterface
        /// </summary>
        /// <param name="kaEvent"></param>
        public void SaveLocal(KaEvent kaEvent)
        {
            database.save(kaEvent);
        }

        /// <summary>
        /// Synchronisieren der Datenbanken
        /// Hier wird das Update durchgeführt
        /// </summary>
        public void SyncData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines wether a Request was a success or not
        /// It's possible to increase its power
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        bool RequestResolve(Package package)
        {
            if (package.request == Request.Success)
                return true;
            else
                return false;
        }
    }
}

