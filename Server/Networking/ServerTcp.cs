using Shared.Enums;
using Shared.Models;
using Shared.Networking;
using System;
using System.Collections.Generic;

namespace Server.Networking
{
    public partial class ServerTcp
    {
        // SLoginOk = 3
        public void SendLoginOk(int clientId, bool success, string message, string accountId)
        {
            using var writer = new PacketWriter((int)ServerPacketId.SLoginOk);
            writer.Write(success);
            writer.Write(message ?? "");
            writer.Write(accountId ?? "");
            SendBytes(clientId, writer.ToArray());
        }

        // SAllChars = 2
        public void SendAllChars(int clientId, List<CharacterSummary> chars)
        {
            using var writer = new PacketWriter((int)ServerPacketId.SAllChars);
            writer.Write(chars.Count);
            foreach (var c in chars)
            {
                writer.Write(c.Id);
                writer.Write(c.Name);
                writer.Write(c.Level);
                writer.Write(c.ClassId); // only send ID
            }
            SendBytes(clientId, writer.ToArray());
        }
        public void SendAlert(int clientId, string msg)
        {
            using var w = new PacketWriter((int)ServerPacketId.SAlertMsg);
            w.Write(msg ?? "");
            SendBytes(clientId, w.ToArray());
        }
        public void SendInGame(int clientId)
        {
            using var w = new PacketWriter((int)ServerPacketId.SInGame);
            SendBytes(clientId, w.ToArray());
        }
        public void SendSync(int clientId, string msg)
        {
            using var w = new PacketWriter((int)ServerPacketId.SSync);
            w.Write(msg ?? "");
            SendBytes(clientId, w.ToArray());
        }
        public void SendSayMsg(int clientId, string from, string message)
        {
            using var writer = new PacketWriter((int)ServerPacketId.SSayMsg);
            writer.Write(from);
            writer.Write(message);
            SendBytes(clientId, writer.ToArray());
        }

        public void SendPlayerData(int clientId, Character character, long nextLevelExperience)
        {
            using var writer = new PacketWriter((int)ServerPacketId.SPlayerData);
            writer.Write(character.Name);
            writer.Write(character.ClassId);
            writer.Write(character.Level);
            writer.Write(character.Experience);
            writer.Write(nextLevelExperience);
            SendBytes(clientId, writer.ToArray());
        }

        public void SendPlayerStats(int clientId, int strength, int defense, int magi, int speed, int critHit, int blockChance)
        {
            using var writer = new PacketWriter((int)ServerPacketId.SPlayerStats);
            writer.Write(strength);
            writer.Write(defense);
            writer.Write(magi);
            writer.Write(speed);
            writer.Write(critHit);
            writer.Write(blockChance);
            SendBytes(clientId, writer.ToArray());
        }

        public void SendPlayerHp(int clientId, int current, int max)
        {
            using var writer = new PacketWriter((int)ServerPacketId.SPlayerHp);
            writer.Write(current);
            writer.Write(max);
            SendBytes(clientId, writer.ToArray());
        }

        public void SendPlayerMp(int clientId, int current, int max)
        {
            using var writer = new PacketWriter((int)ServerPacketId.SPlayerMp);
            writer.Write(current);
            writer.Write(max);
            SendBytes(clientId, writer.ToArray());
        }

        public void SendPlayerStamina(int clientId, int current, int max)
        {
            using var writer = new PacketWriter((int)ServerPacketId.SPlayerStamina);
            writer.Write(current);
            writer.Write(max);
            SendBytes(clientId, writer.ToArray());
        }
    }
}
