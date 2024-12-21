namespace Subnautica.Events.EventArgs
{
    using System;

    using static NotificationManager;

    public class NotificationToggleEventArgs : EventArgs
    {
        public NotificationToggleEventArgs(Group group, string key, bool isAdded)
        {
            this.Group   = group;
            this.Key     = key;
            this.IsAdded = isAdded;
        }

        public Group Group { get; set; }

        public string Key { get; set; }

        public bool IsAdded { get; set; }
    }
}
