using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaPlaner.Storage
{
    interface IDatabase
    {

        bool registerUser(string username, string password, string password_bestaetigen);

        bool login(string username, string password);

        //bool Date(string titel, string ort, string tag, string monat, string jahr, string stunde, string minute, string prioritaet, string beschreibung, string haeufigkeit, string beschraenkung, string wochentag, string welcher_tag);

        

    }
}
