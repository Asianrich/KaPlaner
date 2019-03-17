using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaPlaner.Objects;

namespace KaPlaner.Logic
{
    /// <summary>
    /// Interface, dass von der ClientLogic implementiert werden muss
    /// Es entspricht den Anforderungen der GUI an die Logik
    /// </summary>
    public interface IClientLogic
    {
        bool loginLocal(string username, string password);

        bool loginRemote(string username, string password);

        bool registerLocal(string username, string password, string password_bestaetigen);

        bool registerRemote(string username, string password, string password_bestaetigen);

        
    }
}
