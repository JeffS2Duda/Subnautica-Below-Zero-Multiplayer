namespace Subnautica.Events.EventArgs
{
    using System;

    public class LifepodZoneSelectingEventArgs : EventArgs
    {
        public LifepodZoneSelectingEventArgs(string key, bool isAllowed = true)
        {
            this.Key       = key;
            this.IsAllowed = isAllowed;
        }

        public string Key { get; set; }

        public sbyte ZoneId { get; set; } = -1;

        public bool IsAllowed { get; set; }
    }
}
