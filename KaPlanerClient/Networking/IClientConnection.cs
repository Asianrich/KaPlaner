using KaObjects;

namespace KaPlaner.Networking
{
    public interface IClientConnection
    {
        Package Start(Package state);
        void ChangeIP(string ipAddress);
        void ChangeIP(System.Net.IPAddress iPAddress);
        void ChangeP2P();
        System.Net.IPAddress GetIPAddress();
    }
}
