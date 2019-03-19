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
        bool LoginLocal(User user);

        bool LoginRemote(User user);

        bool RegisterLocal(User user, string password_bestaetigen);

        bool RegisterRemote(User user, string password_bestaetigen);

        void SaveLocal(KaEvent kaEvent);

        void SyncData();
    }
}