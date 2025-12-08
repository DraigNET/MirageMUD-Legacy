using System.Collections.Generic;
using Shared.Models;

namespace Server.Database
{
    public interface IAccountRepository
    {
        Account? GetByUsername(string username);
        Account? GetByEmail(string email);

        bool Exists(string username);
        Account? Create(string username, string email, string password, out string? error);
        void Save(Account account);

        List<CharacterSummary> GetCharacters(string username);
        bool AddCharacter(string username, Character character, out string? error);
        bool DeleteCharacter(string username, string charId);
        bool CharacterBelongsTo(string username, string charId);

        // 🔧 Add these back
        bool IsAccountNameAvailable(string username);
        Account? CreateAccount(string username, string email, string password);
    }
}