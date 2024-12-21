namespace Subnautica.Events.EventArgs
{
    using System;

    public class CreatureFreezingEventArgs : EventArgs
    {
        public CreatureFreezingEventArgs(string uniqueId, float lifeTime, string brinicleId = null, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.LifeTime = lifeTime;
            this.BrinicleId = brinicleId;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public float LifeTime { get; set; }

        public string BrinicleId { get; set; }

        public bool IsAllowed { get; set; }
    }
}
