using KaObjects;

namespace KaPlaner.Networking
{
    public interface IClientConnection
    {
        Package Start(Package state);
        void changeIP(string ipAddress);
    }
}
