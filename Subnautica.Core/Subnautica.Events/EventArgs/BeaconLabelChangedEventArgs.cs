namespace Subnautica.Events.EventArgs
{
    using System;

    public class BeaconLabelChangedEventArgs : EventArgs
    {
        public BeaconLabelChangedEventArgs(string uniqueId, string text)
        {
            this.UniqueId = uniqueId;
            this.Text     = text;
        }

        public string UniqueId { get; set; }

        public string Text { get; set; }
    }
}
