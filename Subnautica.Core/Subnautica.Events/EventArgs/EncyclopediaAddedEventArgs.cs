namespace Subnautica.Events.EventArgs
{
    using System;

    public class EncyclopediaAddedEventArgs : EventArgs
    {
        public EncyclopediaAddedEventArgs(string key, bool verbose)
        {
            this.Key = key;
            this.Verbose = verbose;
        }

        public string Key { get; private set; }

        public bool Verbose { get; private set; }
    }
}
