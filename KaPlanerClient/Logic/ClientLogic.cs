using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KaPlaner.Storage;
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
        IDatabase database = new Database();
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
        public bool loginRemote(User user)
        {
            throw new NotImplementedException();
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
        public bool registerRemote(User user, string password_bestaetigen)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Speichern des mitgelieferten Events in der Datenbank
        /// </summary>
        /// <param name="kaEvent"></param>
        public void saveLocal(KaEvent kaEvent)
        {
            database.save(kaEvent);
        }
    }
}

