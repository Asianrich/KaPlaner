﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaObjects;

namespace KaPlaner.Networking
{
    public interface IClientConnection
    {
        Package Start(Package state);
        void changeIP(string ipAddress);
    }
}