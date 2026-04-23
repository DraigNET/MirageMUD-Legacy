using Server.Models;
using Shared.Networking;

namespace Server.Networking
{
    public partial class ServerTcp
    {
        public void SendRoomSnapshot(int clientId, RoomSnapshot room, bool showNarration = true)
        {
            using var w = new PacketWriter((int)ServerPacketId.SRoomData);

            w.Write(room.RoomId);
            w.Write(room.Name);
            w.Write(room.Description);
            w.Write(showNarration);

            // Exits
            w.Write(room.Exits.Count);
            foreach (var exit in room.Exits)
                w.Write(exit);

            // Players
            w.Write(room.Players.Count);
            foreach (var player in room.Players)
                w.Write(player);

            // NPCs
            w.Write(room.Npcs.Count);
            foreach (var npc in room.Npcs)
            {
                w.Write(npc.InstanceId);
                w.Write(npc.DisplayName);
            }

            // Items
            w.Write(room.Items.Count);
            foreach (var item in room.Items)
                w.Write(item);

            SendBytes(clientId, w.ToArray());

            // End-of-room marker (Mirage-style)
            using var done = new PacketWriter((int)ServerPacketId.SRoomDone);
            SendBytes(clientId, done.ToArray());
        }
    }
}
