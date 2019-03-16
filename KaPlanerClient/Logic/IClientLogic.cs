using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace KaPlaner.Logic
{
    /// <summary>
    /// Interface, dass von der ClientLogic implementiert werden muss
    /// Es entspricht den Anforderungen der GUI an die Logik
    /// </summary>
    public interface IClientLogic
    {
        bool registerUser(string username, string password, string password_bestaetigen);

        bool login(string username, string password);
    }
}
