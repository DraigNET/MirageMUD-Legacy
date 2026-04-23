using Client.App;
using Client.Services;
using Shared.Enums;
using Shared.Models;
using Shared.Networking;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client.Game
{
    public static class DataHandler
    {
        public static void Handle(PacketReader reader, NetworkClient client)
        {
            switch ((ServerPacketId)reader.Id)
            {
                case ServerPacketId.SAlertMsg:
                    {
                        string alert = reader.ReadString();
                        ClientUI.OnUI(() =>
                        {
                            if (ClientUI.Game != null)
                            {
                                ClientUI.Game.AppendChatLine($"System: {alert}");
                            }
                            else
                            {
                                MessageBox.Show(alert, "Server Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        });
                        break;
                    }

                case ServerPacketId.SSayMsg:
                    {
                        string from = reader.ReadString();
                        string message = reader.ReadString();

                        ClientUI.OnUI(() =>
                        {
                            ClientUI.Game?.AppendChatLine($"{from}: {message}");
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

                case ServerPacketId.SRoomData:
                    {
                        int roomId = reader.ReadInt();
                        string name = reader.ReadString();
                        string description = reader.ReadString();

                        int exitCount = reader.ReadInt();
                        var exits = new List<string>(exitCount);
                        for (int i = 0; i < exitCount; i++)
                            exits.Add(reader.ReadString());

                        int playerCount = reader.ReadInt();
                        var players = new List<string>(playerCount);
                        for (int i = 0; i < playerCount; i++)
                            players.Add(reader.ReadString());

                        int npcCount = reader.ReadInt();
                        var npcs = new List<NpcInstanceView>(npcCount);
                        for (int i = 0; i < npcCount; i++)
                        {
                            var instanceId = reader.ReadString();
                            var displayName = reader.ReadString();

                            npcs.Add(new NpcInstanceView
                            {
                                InstanceId = instanceId,
                                DisplayName = displayName
                            });
                        }

                        int itemCount = reader.ReadInt();
                        var items = new List<string>(itemCount);
                        for (int i = 0; i < itemCount; i++)
                            items.Add(reader.ReadString());

                        ClientUI.OnUI(() =>
                        {
                            ClientUI.Game?.ApplyRoomSnapshot(
                                roomId,
                                name,
                                description,
                                exits,
                                players,
                                npcs,
                                items
                            );
                        });

                        break;
                    }

                case ServerPacketId.SPlayerData:
                    {
                        var name = reader.ReadString();
                        var classId = reader.ReadInt();
                        var level = reader.ReadInt();
                        var experience = reader.ReadLong();
                        var nextLevelExperience = reader.ReadLong();

                        ClientUI.OnUI(() =>
                        {
                            ClientUI.Game?.ApplyPlayerData(name, classId, level, experience, nextLevelExperience);
                        });

                        break;
                    }

                case ServerPacketId.SPlayerStats:
                    {
                        var strength = reader.ReadInt();
                        var defense = reader.ReadInt();
                        var magi = reader.ReadInt();
                        var speed = reader.ReadInt();
                        var critHit = reader.ReadInt();
                        var blockChance = reader.ReadInt();

                        ClientUI.OnUI(() =>
                        {
                            ClientUI.Game?.ApplyPlayerStats(strength, defense, magi, speed, critHit, blockChance);
                        });

                        break;
                    }

                case ServerPacketId.SPlayerHp:
                    {
                        var current = reader.ReadInt();
                        var max = reader.ReadInt();

                        ClientUI.OnUI(() => ClientUI.Game?.ApplyHp(current, max));
                        break;
                    }

                case ServerPacketId.SPlayerMp:
                    {
                        var current = reader.ReadInt();
                        var max = reader.ReadInt();

                        ClientUI.OnUI(() => ClientUI.Game?.ApplyMp(current, max));
                        break;
                    }

                case ServerPacketId.SPlayerStamina:
                    {
                        var current = reader.ReadInt();
                        var max = reader.ReadInt();

                        ClientUI.OnUI(() => ClientUI.Game?.ApplyStamina(current, max));
                        break;
                    }

                case ServerPacketId.SRoomDone:
                    {
                        // Mirage-style end-of-room marker.
                        // We don't need to do anything here yet.
                        break;
                    }

                case ServerPacketId.SInGame:
                    ClientUI.OnUI(() =>
                    {
                        var main = ClientUI.MainMenu!;
                        var game = new Client.Forms.GameForm(client);

                        ClientUI.Game = game;

                        game.FormClosed += (_, __) =>
                        {
                            ClientUI.Game = null;
                            main.Show();
                        };

                        main.Hide();
                        game.Show();
                    });
                    break;

                default:
                    {
                        Console.WriteLine($"[Client] Unhandled server packet: {reader.Id}");
                        break;
                    }
            }
        }
    }
}
