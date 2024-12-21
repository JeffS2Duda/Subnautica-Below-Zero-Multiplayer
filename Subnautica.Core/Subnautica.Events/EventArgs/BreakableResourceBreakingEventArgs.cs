namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class BreakableResourceBreakingEventArgs : EventArgs
    {
        public BreakableResourceBreakingEventArgs(string uniqueId, TechType techType, Vector3 position, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.TechType = techType;
            this.Position = position;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public Vector3 Position { get; set; }

        public TechType TechType { get; set; }

        public bool IsAllowed { get; set; }
    }
}
