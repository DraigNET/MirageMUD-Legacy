using Client.Services;
using Shared.Enums;
using Shared.Models;
using Shared.Networking;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class GameForm : Form
    {
        private readonly NetworkClient _client;
        private string? _selectedNpcInstanceId;

        private sealed record NpcListItem(string InstanceId, string DisplayName)
        {
            public override string ToString() => DisplayName;
        }

        public GameForm(NetworkClient client)
        {
            InitializeComponent();

            _client = client;

            KeyPreview = true;
            KeyDown += GameForm_KeyDown;
            txtMyChat.KeyDown += TxtMyChat_KeyDown;
            lstNPCs.SelectedIndexChanged += LstNPCs_SelectedIndexChanged;
        }

        private async void GameForm_KeyDown(object? sender, KeyEventArgs e)
        {
            Direction? dir = e.KeyCode switch
            {
                Keys.Up => Direction.North,
                Keys.Right => Direction.East,
                Keys.Down => Direction.South,
                Keys.Left => Direction.West,
                _ => null
            };

            if (dir is null)
                return;

            try
            {
                await Client.Game.ClientGameLogic.SendPlayerMove(_client, dir.Value);
            }
            catch
            {
                // ignore for now
            }
        }

        private async void TxtMyChat_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;

            var text = txtMyChat.Text.Trim();
            if (text.Length == 0)
                return;

            txtMyChat.Clear();

            try
            {
                await Client.Game.ClientGameLogic.SendSay(_client, text);
            }
            catch
            {
                // ignore for now
            }
        }

        public void ApplyRoomSnapshot(
            int roomId,
            string name,
            string description,
            IReadOnlyList<string> exits,
            IReadOnlyList<string> players,
            IReadOnlyList<NpcInstanceView> npcs,
            IReadOnlyList<string> items)
        {
            var previousNpcId = _selectedNpcInstanceId;

            lblRoomNum.Text = roomId.ToString();

            lstPlayers.BeginUpdate();
            lstPlayers.Items.Clear();
            foreach (var p in players)
                lstPlayers.Items.Add(p);
            lstPlayers.EndUpdate();

            lstNPCs.BeginUpdate();
            lstNPCs.Items.Clear();
            foreach (var n in npcs)
                lstNPCs.Items.Add(new NpcListItem(n.InstanceId, n.DisplayName));
            lstNPCs.EndUpdate();

            if (!string.IsNullOrWhiteSpace(previousNpcId))
            {
                for (var i = 0; i < lstNPCs.Items.Count; i++)
                {
                    if (lstNPCs.Items[i] is NpcListItem npc && npc.InstanceId == previousNpcId)
                    {
                        lstNPCs.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (lstNPCs.SelectedItem is not NpcListItem)
                ClearNpcTarget();

            lstItems.BeginUpdate();
            lstItems.Items.Clear();
            foreach (var i in items)
                lstItems.Items.Add(i);
            lstItems.EndUpdate();

            AppendChatLine($"[{roomId}] {name}");
            if (!string.IsNullOrWhiteSpace(description))
                AppendChatLine(description);

            if (exits.Count > 0)
                AppendChatLine($"Exits: {string.Join(", ", exits)}");
            else
                AppendChatLine("Exits: none");

            AppendChatLine(string.Empty);
        }

        public void ApplyPlayerData(string name, int classId, int level, long experience, long nextLevelExperience)
        {
            lblLevel.Text = level.ToString("00");
            lblXP.Text = $"{experience}/{nextLevelExperience}";
            Text = $"{name} - {((CharacterClass)classId)} - MirageMUD";
        }

        public void ApplyPlayerStats(int strength, int defense, int magi, int speed, int critHit, int blockChance)
        {
            lblStr.Text = strength.ToString("00");
            lblDef.Text = defense.ToString("00");
            lblMagi.Text = magi.ToString("00");
            lblSpeed.Text = speed.ToString("00");
            lblCritHit.Text = critHit.ToString("00");
            lblBlockChance.Text = blockChance.ToString("00");
        }

        public void ApplyHp(int current, int max) => lblHP.Text = $"{current}/{max}";
        public void ApplyMp(int current, int max) => lblMP.Text = $"{current}/{max}";
        public void ApplyStamina(int current, int max) => lblStamina.Text = $"{current}/{max}";
        public string? GetSelectedNpcInstanceId() => _selectedNpcInstanceId;

        public void AppendChatLine(string text)
        {
            if (txtChat.TextLength > 0)
                txtChat.AppendText(Environment.NewLine);

            txtChat.AppendText(text ?? string.Empty);
            txtChat.SelectionStart = txtChat.TextLength;
            txtChat.ScrollToCaret();
        }

        private void LstNPCs_SelectedIndexChanged(object? sender, System.EventArgs e)
        {
            if (lstNPCs.SelectedItem is NpcListItem npc)
            {
                _selectedNpcInstanceId = npc.InstanceId;
                lblTarget.Text = npc.DisplayName;
            }
            else
            {
                ClearNpcTarget();
            }
        }

        private void ClearNpcTarget()
        {
            _selectedNpcInstanceId = null;
            lblTarget.Text = string.Empty;
        }
    }
}
