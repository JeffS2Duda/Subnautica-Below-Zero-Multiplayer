namespace Subnautica.Events.EventArgs
{
    using System;

    public class JukeboxDiskAddedEventArgs : EventArgs
    {
        public JukeboxDiskAddedEventArgs(string trackFile, bool notify)
        {
            TrackFile = trackFile;
            Notify = notify;
        }

        public string TrackFile { get; private set; }

        public bool Notify { get; private set; }
    }
}
