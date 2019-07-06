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
        string IpString { get; set; }
        void ResolvePackage(Package package);
        List<System.Net.IPAddress> ResolvePackage(P2PPackage package);
        //List<string> resolvePackage(HPackage package);
        Package Forwarding(Package package);
        void Settings();

        Package resolvePackages(List<Package> packages);
    }
}
