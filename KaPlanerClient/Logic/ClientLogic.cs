using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KaPlaner.Storage;
using KaPlaner.Networking;
using KaPlaner.Objects;

namespace KaPlaner.Logic
{
    /// <summary>
    /// Client Logic, die Eingaben der GUI annimmt und überprüft, and die DB weitergibt und
    /// die Verbindung zum Server verwaltet.
    /// </summary>
    public class ClientLogic : IClientLogic
    {
        IDatabase database = new Database();

        /// <summary>
        /// Login mit Nutzernamen und Password
        /// Benutzt das Datenbankinterface
        /// TODO: Weitere Kontrollen
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool loginLocal(string username, string password)
        {
            return database.login(username, password);
        }

        /// <summary>
        /// Login mit Nutzernamen und Passwort
        /// Benutzt das Networkinterface
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool loginRemote(string username, string password)
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
        public bool registerLocal(string username, string password, string password_bestaetigen)
        {
            return database.registerUser(username, password, password_bestaetigen);
        }

        /// <summary>
        /// Registrierung eines Nutzers auf Server
        /// Benutzt das Networkinterface
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="password_bestaetigen"></param>
        /// <returns></returns>
        public bool registerRemote(string username, string password, string password_bestaetigen)
        {
            throw new NotImplementedException();
        }
    }
}

