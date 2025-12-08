using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Shared.Models;
using Shared.Security;

namespace Server.Database
{
    public sealed class JsonAccountRepository : IAccountRepository
    {
        private readonly string _accountsDir;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonAccountRepository(string baseDir)
        {
            _accountsDir = Path.Combine(baseDir, "Accounts");
            Directory.CreateDirectory(_accountsDir);
        }

        private string GetFilePath(string username)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                username = username.Replace(c, '_');
            return Path.Combine(_accountsDir, $"{username}.json");
        }

        public Account? GetByUsername(string username)
        {
            var path = GetFilePath(username);
            if (!File.Exists(path)) return null;
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Account>(json, _options);
        }

        public Account? GetByEmail(string email)
        {
            var file = Directory.EnumerateFiles(_accountsDir, "*.json")
                .FirstOrDefault(f =>
                {
                    var acc = JsonSerializer.Deserialize<Account>(File.ReadAllText(f), _options);
                    return acc?.Email.Equals(email, StringComparison.OrdinalIgnoreCase) ?? false;
                });

            if (file == null) return null;
            return JsonSerializer.Deserialize<Account>(File.ReadAllText(file), _options);
        }

        public bool Exists(string username)
        {
            return File.Exists(GetFilePath(username));
        }

        public Account Create(string username, string email, string password, out string? error)
        {
            error = null;

            if (Exists(username))
            {
                error = "Username already exists.";
                return null!;
            }

            if (GetByEmail(email) != null)
            {
                error = "Email already in use.";
                return null!;
            }

            var account = new Account
            {
                Id = Guid.NewGuid().ToString("N"),
                Username = username,
                Email = email,
                PasswordHash = PasswordHasher.Hash(password),
                EmailVerified = true,
                CreatedUtc = DateTime.UtcNow,
                Characters = new List<Character>()
            };

            Save(account);
            return account;
        }

        public void Save(Account account)
        {
            var path = GetFilePath(account.Username);
            var json = JsonSerializer.Serialize(account, _options);
            File.WriteAllText(path, json);
        }

        public List<CharacterSummary> GetCharacters(string username)
        {
            var acc = GetByUsername(username);
            if (acc == null) return new List<CharacterSummary>();

            return acc.Characters
                .Select(CharacterSummary.FromCharacter)
                .ToList();
        }

        public bool AddCharacter(string username, Character character, out string? error)
        {
            error = null;
            var acc = GetByUsername(username);
            if (acc == null)
            {
                error = "Account not found.";
                return false;
            }

            if (acc.Characters.Count >= 5)
            {
                error = "Maximum 5 characters per account.";
                return false;
            }

            acc.Characters.Add(character);
            Save(acc);
            return true;
        }

        public bool DeleteCharacter(string username, string charId)
        {
            var acc = GetByUsername(username);
            if (acc == null) return false;

            var existing = acc.Characters.FirstOrDefault(c => c.Id == charId);
            if (existing == null) return false;

            acc.Characters.Remove(existing);
            Save(acc);
            return true;
        }
        public bool CharacterBelongsTo(string username, string charId)
        {
            var acc = GetByUsername(username);
            if (acc == null) return false;

            return acc.Characters.Any(c => c.Id == charId);
        }
        public bool IsAccountNameAvailable(string username)
            => !Exists(username);

        public Account? CreateAccount(string username, string email, string password)
        {
            string? error;
            return Create(username, email, password, out error);
        }
    }
}