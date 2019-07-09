using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KaObjects.Storage
{
    public interface IDatabase
    {
        bool login(User user);

        bool registerUser(User user, string password_bestaetigen);

        void SaveEvent(KaEvent kaEvent);

        List<KaEvent> LoadEvents(User user, DateTime month);

        List<KaEvent> read(string owner);

        void Delete_date();

        LinkedList<string> GetWellKnownPeers();

        int getUserCount();
        bool getUser(User user);

        void newServerEntry(string ip, int id);

        int getServerCount();

    }
}
