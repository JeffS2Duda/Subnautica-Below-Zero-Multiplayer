namespace Subnautica.Events.EventArgs
{
    using System;

    public class PlayerStatsUpdatedEventArgs : EventArgs
    {
        public PlayerStatsUpdatedEventArgs(float health, float food, float water)
        {
            this.Health = health;
            this.Food = food;
            this.Water = water;
        }

        public float Health { get; }

        public float Food { get; }

        public float Water { get; }
    }
}
