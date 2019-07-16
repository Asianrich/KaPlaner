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
        Package ResolvePackage(Package package);
        void Settings();
        Package resolving(Package package);
    }
}
