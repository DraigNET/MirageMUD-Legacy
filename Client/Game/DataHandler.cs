using Client.App;
using Shared.Enums;
using Shared.Models;
using Shared.Networking;
using System;

namespace Client.Game
{
    public static class DataHandler
    {
        public static void Handle(PacketReader reader)
        {
            switch ((ServerPacketId)reader.Id)
            {
                case ServerPacketId.SAlertMsg:
                    {
                        string alert = reader.ReadString();
                        ClientUI.OnUI(() =>
                        {
                            MessageBox.Show(alert, "Server Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        });
                        break;
                    }
                case ServerPacketId.SSync:
                    {
                        _ = reader.ReadString(); // ignore payload
                        Client.App.ClientUI.OnUI(() => Client.App.ClientUI.MainMenu?.OnPong(DateTime.UtcNow));
                        break;
                    }
                case ServerPacketId.SLoginOk:
                    {
                        bool ok = reader.ReadBool();
                        string msg = reader.ReadString();
                        string accountId = reader.ReadString();

                        if (!ok)
                        {
                            // Show the message on the Login screen
                            ClientUI.OnUI(() => ClientUI.MainMenu!.ShowLoginError(msg));
                            break;
                        }

                        // Save the account id for later commands
                        ClientSession.AccountId = accountId;

                        // Do NOT switch yet. The server follows up with SAllChars.
                        // We'll switch when we actually have the list to show.
                        break;
                    }

                case ServerPacketId.SAllChars:
                    {
                        int count = reader.ReadInt();
                        var chars = new List<CharacterSummary>(count);
                        for (int i = 0; i < count; i++)
                        {
                            var id = reader.ReadString();
                            var name = reader.ReadString();
                            var lvl = reader.ReadInt();
                            var classId = reader.ReadInt();

                            chars.Add(new CharacterSummary
                            {
                                Id = id,
                                Name = name,
                                Level = lvl,
                                ClassId = classId
                            });
                        }

                        ClientUI.OnUI(() => ClientUI.MainMenu!.ShowCharacters(chars));
                        break;
                    }
                default:
                    {
                        Console.WriteLine($"[Client] Unhandled server packet: {reader.Id}");
                        break;
                    }

                case ServerPacketId.SInGame:
                    // Switch to the game window
                    Client.App.ClientUI.OnUI(() =>
                    {
                        var main = Client.App.ClientUI.MainMenu!;
                        var game = new Client.Forms.GameForm();

                        // If you want to return to menu when game closes:
                        game.FormClosed += (_, __) => main.Show();

                        main.Hide();
                        game.Show();
                    });
                    break;
            }
        }
    }
}