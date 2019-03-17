using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KaObjects;

namespace KaPlaner.Storage
{
    interface IDatabase
    {
        bool login(User user);

        bool registerUser(User user, string password_bestaetigen);

        void save(KaEvent kaEvent);
    }
}
