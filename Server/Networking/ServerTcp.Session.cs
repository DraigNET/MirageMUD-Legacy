using System.Collections.Concurrent;

namespace Server.Networking
{
    public partial class ServerTcp
    {
        // Using the existing _clients dictionary from the core partial.

        public void SetAccount(int clientId, string accountId)
        {
            if (_clients.TryGetValue(clientId, out var c))
                c.AccountId = accountId;
        }

        public bool TryGetAccount(int clientId, out string? accountId)
        {
            accountId = null;
            if (_clients.TryGetValue(clientId, out var c))
                accountId = c.AccountId;
            return accountId != null;
        }

        public void SetCharacter(int clientId, string characterId)
        {
            if (_clients.TryGetValue(clientId, out var c))
                c.CharacterId = characterId;
        }

        public bool TryGetCharacter(int clientId, out string? characterId)
        {
            characterId = null;
            if (_clients.TryGetValue(clientId, out var c))
                characterId = c.CharacterId;
            return characterId != null;
        }

        public bool IsCharacterActive(string characterId, int? excludingClientId = null)
        {
            foreach (var kv in _clients)
            {
                if (excludingClientId.HasValue && kv.Key == excludingClientId.Value)
                    continue;

                if (string.Equals(kv.Value.CharacterId, characterId, StringComparison.Ordinal))
                    return true;
            }

            return false;
        }

        public IReadOnlyList<int> GetClientIdsForCharacters(IEnumerable<string> characterIds)
        {
            var ids = new HashSet<string>(characterIds, StringComparer.Ordinal);
            var clientIds = new List<int>();

            foreach (var kv in _clients)
            {
                if (kv.Value.CharacterId is string characterId && ids.Contains(characterId))
                    clientIds.Add(kv.Key);
            }

            return clientIds;
        }

        public IReadOnlyList<(int ClientId, string AccountId, string CharacterId)> GetActiveCharacters()
        {
            var activeCharacters = new List<(int ClientId, string AccountId, string CharacterId)>();

            foreach (var kv in _clients)
            {
                if (kv.Value.AccountId is string accountId && kv.Value.CharacterId is string characterId)
                    activeCharacters.Add((kv.Key, accountId, characterId));
            }

            return activeCharacters;
        }
    }
}
