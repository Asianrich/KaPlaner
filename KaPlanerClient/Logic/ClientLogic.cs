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
        bool test = false;

        public ClientLogic()
        {
          this.test = database.login("Aragorn", "123");

        }

        public bool GetTest() { return test; }
    }
}

