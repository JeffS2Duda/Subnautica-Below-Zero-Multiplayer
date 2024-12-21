namespace Subnautica.Events.EventArgs
{
    using System;

    public class PDALogAddedEventArgs : EventArgs
    {
        public PDALogAddedEventArgs(string key, float timestamp)
        {
            this.Key = key;
            this.Timestamp = timestamp;
        }

        public string Key { get; private set; }

        public float Timestamp { get; private set; }
    }
}
