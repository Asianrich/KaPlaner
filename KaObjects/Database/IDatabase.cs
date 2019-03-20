﻿using System;
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

        List<KaEvent> LoadEvents(DateTime month);

        void Delete_date();
    }
}
