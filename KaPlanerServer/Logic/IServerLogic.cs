
using KaObjects;

namespace KaPlanerServer.Logic
{
    interface IServerLogic
    {
        Package ResolvePackage(Package package);
        void Settings();
        Package Resolving(Package package);
    }
}
