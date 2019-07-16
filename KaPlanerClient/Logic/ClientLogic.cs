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
        // Connection String muss noch angepasst werden
        //static readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Yoshi\\source\\repos\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Malak\\source\\repos\\Asianrich\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\source\\repos\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Richard\\source\\repos\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        //static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\manhk\\source\\repos\\KaPlaner\\KaPlanerClient\\Data\\User_Calendar.mdf;Integrated Security=True";
        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True";



        IDatabase database = new Database(connectionString);
        IClientConnection clientConnection = new ClientConnection();
        public User currentUser; //Sollte auf dem Server verwaltet werden aus Sicherheitsgründen.

        public List<KaEvent> eventList = new List<KaEvent>();
        public List<KaEvent> inviteList = new List<KaEvent>();

        public List<KaEvent> getInvites()
        {
            return inviteList;
        }
        public List<KaEvent> GetEventList()
        {
            return eventList;
        }
        public User GetUser()
        {
            return currentUser;
        }
        /// <summary>
        /// Läd eine Liste an Events für einen Monat lokal von der Datenbank
        /// Nutzt das Datenbankinterface
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<KaEvent> LoadEventsLocal(DateTime month)
        {
            //return database.LoadEvents(currentUser, month);
            return database.read(currentUser.name);
        }

        /// <summary>
        /// Läd eine Liste an Events für einen Monat Remote von der Datenbank
        /// Nutzt das Datenbankinterface
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<KaEvent> LoadEventsRemote(DateTime month)
        {
            KaEvent eventMonth = new KaEvent();
            eventMonth.Beginn = month;

            Package returnPackage;
            Package loadPackage = new Package(Request.Load, currentUser, eventMonth);

            returnPackage = clientConnection.Start(loadPackage);

            if (returnPackage.request == Request.Success)
                return returnPackage.kaEvents;
            else
                return null;
        }

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



        public void sendInvites(KaEvent kaEvent)
        {

            Package package = new Package();
            package.kaEvents = new List<KaEvent>();
            package.kaEvents.Add(kaEvent);
            package.request = Request.Invite;
            clientConnection.Start(package);

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
            currentUser = user;

            Package returnPackage;
            Package loginPackage = new Package(Request.Login, currentUser);

            returnPackage = clientConnection.Start(loginPackage);

            if (returnPackage.request == Request.changeServer)
            {
                clientConnection.changeIP(returnPackage.sourceServer);
                loginPackage.serverSwitched = true;
                returnPackage = clientConnection.Start(loginPackage);
            }



            if (returnPackage.kaEvents != null)
            {
                eventList = returnPackage.kaEvents;
            }
            if (returnPackage.invites != null)
            {
                inviteList = returnPackage.invites;
            }

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
            if (database.registerUser(user, password_bestaetigen))
            {
                currentUser = user;
                return true;
            }
            else
            {
                return false;
            }
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
            registerPackage.serverSwitched = false;
            clientConnection.changeIP("192.168.0.3"); // für Root und so muss mans ändern
            returnPackage = clientConnection.Start(registerPackage);

            if (returnPackage.request == Request.changeServer)
            {
                clientConnection.changeIP(returnPackage.sourceServer);
                registerPackage.serverSwitched = true;
                returnPackage = clientConnection.Start(registerPackage);
            }

            if (RequestResolve(returnPackage))
            {
                currentUser = returnPackage.user;

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Speichern des mitgelieferten Events in der Datenbank
        /// Benutzt das Datenbankinterface
        /// </summary>
        /// <param name="kaEvent"></param>
        public void SaveLocal(KaEvent kaEvent)
        {
            kaEvent.owner = currentUser;
            database.SaveEvent(kaEvent);
        }

        /// <summary>
        /// Speichern des mitgelieferten Events in der Remote Datenbank
        /// Benutzt das Datenbankinterface
        /// </summary>
        /// <param name="kaEvent"></param>
        public KaEvent SaveRemote(KaEvent kaEvent)
        {
            kaEvent.owner = currentUser;

            Package savePackage = new Package(Request.Save, kaEvent);

            Package returnpackage = clientConnection.Start(savePackage);
            return returnpackage.kaEvents[0];
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

        public void answerInvite(KaEvent kaEvent, bool choice)
        {
            Package package = new Package();
            package.kaEvents = new List<KaEvent>();
            package.user = currentUser;
            package.answerInvite = choice;
            package.request = Request.answerInvite;
            package.kaEvents.Add(kaEvent);
            clientConnection.Start(package);

        }
    }

    /// <summary>
    /// This provides every GUI component access to the same ClientLogic
    /// </summary>
    public class ClientActivator
    {
        public static ClientLogic clientLogic = new ClientLogic();
    }
}

