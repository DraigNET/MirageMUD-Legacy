using Server.Models;
using Server.World;
using Shared.Enums;

namespace Server.Game
{
    public sealed class EnterGameService
    {
        private readonly ServerGameLogic _logic;
        private readonly WorldService _world;

        public EnterGameService(ServerGameLogic logic, WorldService world)
        {
            _logic = logic;
            _world = world;
        }

        public RoomSnapshot Enter(string accountId, string characterId)
        {
            var character = _logic.GetCharacter(accountId, characterId);
            if (character == null)
                throw new InvalidOperationException("Character not found.");

            var roomId = _world.HasRoom(character.RoomId) ? character.RoomId : 1;
            var direction = Enum.IsDefined(typeof(Direction), character.Direction)
                ? (Direction)character.Direction
                : Direction.South;

            _world.EnterRoom(character.Id, character.Name, roomId, direction);
            _logic.UpdateCharacterLocation(accountId, character.Id, roomId, direction);

            return _world.BuildRoomSnapshot(roomId);
        }
    }
}
