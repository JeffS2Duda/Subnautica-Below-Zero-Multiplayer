namespace Subnautica.Events.EventArgs
{
    using System;

    public class StoryCinematicStartedEventArgs : EventArgs
    {
        public StoryCinematicStartedEventArgs(string cinematicName)
        {
            this.CinematicName = cinematicName;
        }

        public string CinematicName { get; set; }
    }
}
