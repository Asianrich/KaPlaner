using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaObjects;

namespace KaPlaner.Logic
{
    /// <summary>
    /// Interface, dass von der ClientLogic implementiert werden muss
    /// Es entspricht den Anforderungen der GUI an die Logik
    /// </summary>
    public interface IClientLogic
    {
        bool loginLocal(User user);

        void loginRemote(User user);

        bool registerLocal(User user, string password_bestaetigen);

        void registerRemote(User user, string password_bestaetigen);

        void saveLocal(KaEvent kaEvent);
    }
}
