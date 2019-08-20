using System;
using System.Collections.Generic;
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
        static readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True";
        readonly IDatabase database = new Database(connectionString);
        readonly IClientConnection clientConnection = new ClientConnection();
        public User currentUser; //Sollte auf dem Server verwaltet werden aus Sicherheitsgründen.

        public List<KaEvent> eventList = new List<KaEvent>();
        public List<KaEvent> inviteList = new List<KaEvent>();

        public List<KaEvent> GetInvites()
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
            return database.Read(currentUser.name);
        }

        /// <summary>
        /// Läd eine Liste an Events für einen Monat Remote von der Datenbank
        /// Nutzt das Datenbankinterface
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<KaEvent> LoadEventsRemote(DateTime month)
        {
            KaEvent eventMonth = new KaEvent
            {
                Beginn = month
            };

            Package loadPackage = new Package(Request.Load, currentUser, eventMonth);
            Package returnPackage = clientConnection.Start(loadPackage);

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
            return database.Login(currentUser);
        }



        public void SendInvites(KaEvent kaEvent)
        {

            Package package = new Package
            {
                kaEvents = new List<KaEvent>()
            };
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
        public Request LoginRemote(User user)
        {
            currentUser = user;

            Package returnPackage;
            Package loginPackage = new Package(Request.Login, currentUser);

            returnPackage = clientConnection.Start(loginPackage);

            if (returnPackage.request == Request.ChangeServer)
            {
                clientConnection.ChangeIP(returnPackage.sourceServer);
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

            return returnPackage.request;
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
            if (database.RegisterUser(user, password_bestaetigen))
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
        public Request RegisterRemote(User user, string passwordConfirm)
        {
            Package returnPackage;
            Package registerPackage = new Package(user, passwordConfirm)
            {
                serverSwitched = false
            };
            //clientConnection.ChangeIP("192.168.0.3"); // für Root und so muss mans ändern
            returnPackage = clientConnection.Start(registerPackage);

            if (returnPackage.request == Request.ChangeServer)
            {
                clientConnection.ChangeIP(returnPackage.sourceServer);
                registerPackage.serverSwitched = true;
                returnPackage = clientConnection.Start(registerPackage);
            }

            return returnPackage.request;
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

        public void AnswerInvite(KaEvent kaEvent, bool choice)
        {
            Package package = new Package
            {
                kaEvents = new List<KaEvent>(),
                user = currentUser,
                answerInvite = choice,
                request = Request.AnswerInvite
            };
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

