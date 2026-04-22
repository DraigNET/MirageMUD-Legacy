using System;
using Client.Forms;

namespace Client.App
{
    internal static class ClientUI
    {
        public static MainMenuForm? MainMenu { get; private set; }
        public static Client.Forms.GameForm? Game { get; set; }

        public static void Attach(MainMenuForm form) => MainMenu = form;

        public static void OnUI(Action action)
        {
            var f = MainMenu;
            if (f == null) return;
            if (f.InvokeRequired) f.BeginInvoke(action);
            else action();
        }
    }
}