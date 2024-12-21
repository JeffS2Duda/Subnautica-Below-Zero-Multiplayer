namespace Subnautica.Events.EventArgs
{
    using System;

    public class ElevatorInitializedEventArgs : EventArgs
    {
        public ElevatorInitializedEventArgs(Rocket rocket)
        {
            this.Instance = rocket;
        }

        public Rocket Instance { get; set; }
    }
}
