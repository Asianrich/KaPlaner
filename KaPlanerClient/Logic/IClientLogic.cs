using KaObjects;
using System;
using System.Collections.Generic;

namespace KaPlaner.Logic
{
    /// <summary>
    /// Interface, dass von der ClientLogic implementiert werden muss
    /// Es entspricht den Anforderungen der GUI an die Logik
    /// </summary>
    public interface IClientLogic
    {
        User GetUser();
        bool LoginLocal(User user);

        Request LoginRemote(User user);

        bool RegisterLocal(User user, string password_bestaetigen);

        Request RegisterRemote(User user, string password_bestaetigen);

        void SaveLocal(KaEvent kaEvent);

        KaEvent SaveRemote(KaEvent kaEvent);

        void SendInvites(KaEvent kaEvent);

        void AnswerInvite(KaEvent kaEvent, bool choice);

        List<KaEvent> GetInvites();
        List<KaEvent> LoadEventsLocal(DateTime month);

        List<KaEvent> LoadEventsRemote(DateTime month);

        List<KaEvent> GetEventList();
    }
}
