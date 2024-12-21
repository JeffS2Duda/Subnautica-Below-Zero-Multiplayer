namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class ConstructorCraftingEventArgs : EventArgs
    {
        public ConstructorCraftingEventArgs(string uniqueId, TechType techType, Vector3 position, Quaternion rotation, bool isAllowed = true)
        {
            this.UniqueId  = uniqueId;
            this.TechType  = techType;
            this.Position  = position;
            this.Rotation  = rotation;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; private set; }

        public TechType TechType { get; private set; }

        public Vector3 Position { get; private set; }

        public Quaternion Rotation { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
