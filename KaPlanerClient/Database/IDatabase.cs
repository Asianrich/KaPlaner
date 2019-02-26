using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaPlaner.Database
{
    interface IDatabase
    {

        bool registerUser(string username, string password, string password_bestaetigen);

        //bool login(string username, string password);




    }
}
