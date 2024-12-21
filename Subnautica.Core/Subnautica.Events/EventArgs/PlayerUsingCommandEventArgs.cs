namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerUsingCommandEventArgs : EventArgs
    {
        public PlayerUsingCommandEventArgs(string command, string fullCommand, bool isAllowed = true)
        {
            this.Command = command.Trim();
            this.FullCommand = fullCommand.Trim();
            this.IsAllowed = isAllowed;
        }

        public string Command { get; set; }

        public string FullCommand { get; set; }

        public bool IsAllowed { get; set; }
    }
}
