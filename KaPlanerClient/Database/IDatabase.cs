using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KaPlaner.Objects;

namespace KaPlaner.Storage
{
    interface IDatabase
    {

        bool registerUser(string username, string password, string password_bestaetigen);

        bool login(string username, string password);

        void Save(string Title, string Ort, int ganztaegig, DateTime Beginn, DateTime Ende, int Prioritaet, string Beschreibung,
            string Haeufigkeit, int Haeufigkeit_Anzahl, int Immer_Wiederholen, int Wiederholungen, DateTime Wiederholen_bis,
            int XMontag, int XDienstag, int XMittwoch, int XDonnerstag, int XFreitag, int XSamstag, int XSonntag);

        /* So wäre wohl besser
         * void save(KaEvent kaEvent);
         */




    }
}
