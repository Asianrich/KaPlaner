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

        bool ServerExist(int ServerID);

        bool UserExist(User user);

        int CheckMemberList();

        void SaveInvites(List<Package> member, int TerminID);

        List<KaEvent> ReadInvites(string user);

        string getServer(int serverID);

        int getUserCount();

        int GetServerCount();

        LinkedList<string> GetWellKnownPeers();
    
        void newServerEntry(string ip, int id);

        



       

        

        

        

       ;
    }
}
