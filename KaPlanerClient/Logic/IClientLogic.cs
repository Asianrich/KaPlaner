﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaObjects;

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

        bool LoginRemote(User user);

        bool RegisterLocal(User user, string password_bestaetigen);

        bool RegisterRemote(User user, string password_bestaetigen);

        void SaveLocal(KaEvent kaEvent);

        void SaveRemote(KaEvent kaEvent);

        void sendInvites(KaEvent kaEvent);

        void answerInvite(KaEvent kaEvent, bool choice);

        List<KaEvent> getInvites();
        List<KaEvent> LoadEventsLocal(DateTime month);

        List<KaEvent> LoadEventsRemote(DateTime month);

        List<KaEvent> GetEventList();
    }
}
