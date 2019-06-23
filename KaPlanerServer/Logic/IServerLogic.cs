using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KaObjects;

namespace KaPlanerServer.Logic
{
    interface IServerLogic
    {
        void resolvePackage(Package package);
        void resolvePackage(P2PPackage package);
        Package forwarding(Package package);
        void Settings();
    }
}
