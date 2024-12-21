namespace Subnautica.Events.EventArgs
{
    using System;

    public class StoryCinematicCompletedEventArgs : EventArgs
    {
        public StoryCinematicCompletedEventArgs(string cinematicName)
        {
            this.CinematicName = cinematicName;
        }

        public string CinematicName { get; set; }
    }
}
