namespace Subnautica.Events.EventArgs
{
    using System;

    public class LifepodZoneCheckEventArgs : EventArgs
    {
        public LifepodZoneCheckEventArgs(string key, bool isAllowed = true)
        {
            this.Key = key;
            this.IsAllowed = isAllowed;
        }

        public string Key { get; set; }

        public bool IsAllowed { get; set; }
    }
}
