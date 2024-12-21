namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Features;

    public class PlayerItemPickedUpEventArgs : EventArgs
    {
        public PlayerItemPickedUpEventArgs(string uniqueId, TechType techType, Pickupable pickupable, bool isAllowed = true)
        {
            this.UniqueId   = uniqueId;
            this.TechType   = techType;
            this.Pickupable = pickupable;
            this.IsAllowed  = isAllowed;
            this.IsStaticWorldEntity = Network.StaticEntity.IsStaticEntity(uniqueId);
        }

        public string UniqueId { get; private set; }

        public TechType TechType { get; private set; }

        public Pickupable Pickupable { get; private set; }

        public bool IsAllowed { get; set; }

        public bool IsStaticWorldEntity { get; private set; }
    }
}
