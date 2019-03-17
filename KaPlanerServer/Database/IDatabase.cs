using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaPlaner.Database
{
    interface IDatabase
    {

        bool registerUser(string username, string password, string password_bestaetigen);

        bool login(string username, string password);

        void Save(string Title, string Ort, int ganztaegig, DateTime Beginn, DateTime Ende, int Prioritaet, string Beschreibung,
             string Haeufigkeit, int Haeufigkeit_Anzahl, int Immer_Wiederholen, int Wiederholungen, DateTime Wiederholen_bis,
             int XMontag, int XDienstag, int XMittwoch, int XDonnerstag, int XFreitag, int XSamstag, int XSonntag);




    }
}
