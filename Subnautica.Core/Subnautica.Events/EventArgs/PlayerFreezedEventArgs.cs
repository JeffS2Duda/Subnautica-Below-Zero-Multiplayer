namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerFreezedEventArgs : EventArgs
    {
        public PlayerFreezedEventArgs(float endTime)
        {
            this.EndTime = endTime;
        }

        public float EndTime { get; set; }
    }
}
