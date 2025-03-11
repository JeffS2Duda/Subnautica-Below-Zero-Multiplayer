namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using System;
    using UnityEngine;

    public class PlayerItemPickedUpEventArgs : EventArgs
    {
        public PlayerItemPickedUpEventArgs(string uniqueId, TechType techType, Pickupable pickupable, bool result = true, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.WaterParkId = ZeroPlayer.CurrentPlayer.GetCurrentWaterParkUniqueId();
            this.ItemWaterParkId = this.GetItemWaterParkId(pickupable);
            this.TechType = techType;
            this.Pickupable = pickupable;
            this.Result = result;
            this.IsAllowed = isAllowed;
            this.IsStaticWorldEntity = Network.StaticEntity.IsStaticEntity(uniqueId);
        }

        public string UniqueId { get; private set; }

        public string WaterParkId { get; private set; }

        public string ItemWaterParkId { get; private set; }

        public TechType TechType { get; private set; }

        public Pickupable Pickupable { get; private set; }

        public bool Result { get; set; }

        public bool IsAllowed { get; set; }

        public bool IsStaticWorldEntity { get; private set; }

        private string GetItemWaterParkId(Pickupable pickupable)
        {
            if (pickupable.transform.parent == null)
                return (string)null;
            WaterPark componentInParent = ((Component)((Component)pickupable).transform).GetComponentInParent<WaterPark>();
            if (componentInParent == null)
                return (string)null;
            BaseDeconstructable baseDeconstructable = componentInParent.GetBaseDeconstructable();
            string itemWaterParkId;
            if (baseDeconstructable == null)
            {
                itemWaterParkId = (string)null;
            }
            else
            {
                GameObject gameObject = ((Component)baseDeconstructable).gameObject;
                itemWaterParkId = gameObject != null ? gameObject.GetIdentityId() : (string)null;
            }
            return itemWaterParkId;
        }
    }
}
