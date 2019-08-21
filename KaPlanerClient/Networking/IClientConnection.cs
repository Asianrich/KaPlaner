using KaObjects;

namespace KaPlaner.Networking
{
    public interface IClientConnection
    {
        Package Start(Package state);
        void ChangeIP(string ipAddress);
        void changeP2P();
    }
}
