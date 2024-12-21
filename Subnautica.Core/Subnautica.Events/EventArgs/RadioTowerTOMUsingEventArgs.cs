namespace Subnautica.Events.EventArgs
{
    using System;

    public class RadioTowerTOMUsingEventArgs : EventArgs
    {
        public RadioTowerTOMUsingEventArgs(string uniqueId, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
