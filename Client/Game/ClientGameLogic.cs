using Client.Services;
using Shared.Enums;
using Shared.Networking;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Game
{
    public static class ClientGameLogic
    {
        public static async Task SendSync(NetworkClient client)
        {
            await client.SendAsync((int)ClientPacketId.CSync, writer =>
            {
                writer.Write("Hello from client");
            });
        }

        public static async Task SendLogin(NetworkClient client, string user, string pass, string ver = "dev")
        {
            await client.SendAsync((int)ClientPacketId.CLogin, writer =>
            {
                writer.Write(user);
                writer.Write(pass);
                writer.Write(ver);
            });
        }

        public static async Task UseCharacter(NetworkClient client, string characterId)
        {
            await client.SendAsync((int)ClientPacketId.CUseChar, w => w.Write(characterId)); // server: ReadString()
        }

        public static async Task DeleteCharacter(NetworkClient client, string characterId)
        {
            await client.SendAsync((int)ClientPacketId.CDelChar, w => w.Write(characterId)); // server: ReadString()
        }

        public static async Task SendNewAccount(NetworkClient client, string username, string email, string password)
        {
            await client.SendAsync((int)ClientPacketId.CNewAccount, writer =>
            {
                writer.Write(username);
                writer.Write(email);
                writer.Write(password);
            });
        }
        public static async Task SendNewCharacter(NetworkClient client, string name, CharacterClass @class, Gender gender, int avatar)
        {
            await client.SendAsync((int)ClientPacketId.CAddChar, writer =>
            {
                writer.Write(name);
                writer.Write((int)@class);
                writer.Write((int)gender);
                writer.Write(avatar);
            });
        }
    }
}