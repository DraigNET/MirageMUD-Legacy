using Client.Services;
using Shared.Networking;

namespace Client.App
{
    public static class ClientSession
    {
        public static NetworkClient? Client { get; private set; }
        public static string? AccountId { get; set; }

        public static void SetClient(NetworkClient client)
        {
            Client = client;
        }
    }
}