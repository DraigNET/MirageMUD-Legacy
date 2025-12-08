using Server.Database;
using Shared.Data;
using Shared.Enums;
using Shared.Models;
using Shared.Networking;
using Shared.Security;
using System.Text;

namespace Server.Game
{
    public sealed class ServerGameLogic
    {
        private readonly IAccountRepository _accounts;

        public ServerGameLogic(IAccountRepository accounts)
        {
            _accounts = accounts;
        }

        public List<CharacterSummary> GetCharacters(string username)
            => _accounts.GetCharacters(username);

        public bool CharacterBelongsTo(string username, string characterId)
            => _accounts.CharacterBelongsTo(username, characterId);

        public bool DeleteCharacter(string username, string characterId)
            => _accounts.DeleteCharacter(username, characterId);

        public LoginResponseDto HandleLogin(LoginRequestDto req)
        {
            // Try username first, then email
            var account = _accounts.GetByUsername(req.UsernameOrEmail)
                        ?? _accounts.GetByEmail(req.UsernameOrEmail);

            if (account is null)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Account not found."
                };
            }

            if (!PasswordHasher.Verify(req.Password, account.PasswordHash))
            {
                Console.WriteLine("---- LOGIN DEBUG ----");
                Console.WriteLine("UsernameOrEmail: '" + req.UsernameOrEmail + "'");
                Console.WriteLine("Password STRING:  '" + req.Password + "'");
                Console.WriteLine("Password BYTES:   " + BitConverter.ToString(Encoding.UTF8.GetBytes(req.Password)));
                Console.WriteLine("Stored hash:      '" + account.PasswordHash + "'");

                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid credentials."
                };
            }

            // (Optional) require email verification
            // if (!account.EmailVerified) { ... }

            var chars = _accounts.GetCharacters(account.Username);

            return new LoginResponseDto
            {
                Success = true,
                Message = "OK",
                AccountId = account.Username, // now using Username as key
                Characters = chars
            };
        }

        public Account? CreateAccount(string username, string email, string password, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
            {
                error = "Username must be at least 3 characters.";
                return null;
            }

            // Keep email check simple unless you already have a validator
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            {
                error = "Please enter a valid email.";
                return null;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 5)
            {
                error = "Password must be at least 5 characters.";
                return null;
            }

            if (!_accounts.IsAccountNameAvailable(username))
            {
                error = "That account already exists.";
                return null;
            }

            return _accounts.CreateAccount(username, email, password);
        }
        public Character? CreateCharacter(
            string accountUsername,
            string name,
            CharacterClass classId,
            Gender gender,
            int avatar,
            out string error)
        {
            error = "";

            // Basic validation
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                error = "Character name must be at least 3 characters.";
                return null;
            }

            // Ensure account exists
            var account = _accounts.GetByUsername(accountUsername);
            if (account == null)
            {
                error = "Account not found.";
                return null;
            }

            if (account.Characters.Count >= 5)
            {
                error = "Maximum characters reached (5).";
                return null;
            }

            // Get class base stats
            if (!ClassDefinitions.Stats.TryGetValue(classId, out var baseStats))
            {
                error = "Invalid class.";
                return null;
            }

            // Create new character
            var character = new Character
            {
                Name = name,
                ClassId = (int)classId,
                Gender = gender,
                Avatar = avatar,
                Level = 1,
                Experience = 0,

                Stats = new[]
                {
                    baseStats.Str,
                    baseStats.Dex,
                    baseStats.Con,
                    baseStats.Int,
                    baseStats.Wis,
                    baseStats.Cha
                },

                Vitals = new int[Enum.GetValues<VitalType>().Length],
                Equipment = new int[Enum.GetValues<EquipmentSlot>().Length]
            };

            // Derive vitals from stats
            character.Vitals[(int)VitalType.HP] = 10 + baseStats.Con * 2;
            character.Vitals[(int)VitalType.Mana] = 5 + baseStats.Int * 2 + baseStats.Wis;
            character.Vitals[(int)VitalType.Stamina] = 5 + baseStats.Con;

            // Save to account
            if (!_accounts.AddCharacter(accountUsername, character, out error))
                return null;

            return character;
        }
    }
}