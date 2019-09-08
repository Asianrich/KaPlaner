using System;
using System.Collections.Generic;


namespace KaObjects.Storage
{
    public interface IDatabase
    {
        bool Login(User user);

        bool RegisterUser(User user, string password_bestaetigen);

        void SaveEvent(KaEvent kaEvent);

        List<KaEvent> LoadEvents(User user, DateTime month);

        List<KaEvent> Read(string owner);

        void DeleteEvent(int TerminID);

        bool ServerExist(int ServerID);

        bool UserExist(string user);

        int CheckMemberList();

        void SaveInvites(string user, KaEvent kaEvent);

        List<KaEvent> ReadInvites(string user);

        string GetServer(int serverID);

        int GetUserCount();

        int GetServerCount();

        //LinkedList<string> GetWellKnownPeers();

        void NewServerEntry(string ip, int id);


        void AnswerInvite(KaEvent kaEvent, string user, bool choice);


    }
}
