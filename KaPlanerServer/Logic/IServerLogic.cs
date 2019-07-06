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
        string ipString { get; set; }
        Package resolvePackage(Package package);
        List<string> resolvePackage(P2PPackage package);
        //List<string> resolvePackage(HPackage package);
        Package forwarding(Package package);
        void Settings();

        Package resolvePackages(List<Package> packages);
    }
}
