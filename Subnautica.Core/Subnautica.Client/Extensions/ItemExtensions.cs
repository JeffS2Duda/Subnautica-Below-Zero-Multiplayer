namespace Subnautica.Client.Extensions
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.MonoBehaviours.Entity;

    using UnityEngine;

    public static class ItemExtensions
    {
        public static bool LocalPickup(this global::Pickupable pickupable)
        {
            using (EventBlocker.Create(ProcessType.ItemPickup))
            {
                return global::Inventory.Get().Pickup(pickupable);
            }
        }

        public static void MultiplayerDrop(this global::Pickupable pickupable, bool callSound = false, bool ignoreTracker = false, string waterParkId = null, double waterParkAddTime = 0)
        {
            pickupable.MultiplayerDrop(pickupable.transform.position, callSound, ignoreTracker, waterParkId, waterParkAddTime);
        }

        public static bool MultiplayerDrop(this global::Pickupable pickupable, Vector3 position, bool callSound = false, bool ignoreTracker = false, string waterParkId = null, double waterParkAddTime = 0)
        {
            if (pickupable == null)
            {
                return false;
            }

            if (callSound)
            {
                if (pickupable.inventoryItem?.container == global::Inventory.main.container || pickupable.inventoryItem?.container == global::Inventory.main.equipment)
                {
                    pickupable.PlayDropSound();
                }
            }

            pickupable.SetVisible(false);
            pickupable.Reparent(null);
            pickupable.Activate(false);
            pickupable.attached = false;
            pickupable.timeDropped = 0.0f;
            pickupable.droppedEvent.Trigger(pickupable);
            pickupable.gameObject.SendMessage("OnDrop", SendMessageOptions.DontRequireReceiver);
            pickupable.transform.position = position;

            if (pickupable.scaler)
            {
                pickupable.scaler.enabled = true;
            }

            if (waterParkId.IsNotNull())
            {
                var waterPark = Network.Identifier.GetComponentByGameObject<global::BaseDeconstructable>(waterParkId)?.GetBaseWaterPark();
                if (waterPark && pickupable.TryGetComponent<global::CreatureEgg>(out var creatureEgg))
                {
                    creatureEgg.progress = 0f;
                    creatureEgg.timeStartHatching = (float)waterParkAddTime;

                    waterPark.AddItem(pickupable);
                }
            }

            SkyEnvironmentChanged.Send(pickupable.gameObject, (GameObject)null);

            if (!ignoreTracker)
            {
                var uniqueId = pickupable.gameObject.GetIdentityId();
                if (uniqueId.IsNotNull())
                {
                    var tracker = ZeroPlayer.CurrentPlayer.GetLocalComponent<MultiplayerEntityTracker>();
                    tracker.Visibility.ToggleChangeEntity(uniqueId);
                }
            }

            return true;
        }

        public static bool MultiplayerPlace(this global::Pickupable pickupable, string baseId)
        {
            pickupable.MultiplayerDrop(pickupable.transform.position, ignoreTracker: true);

            var baseComponent = Network.Identifier.GetGameObject(baseId, true);
            if (baseComponent)
            {
                pickupable.gameObject.transform.parent = baseComponent.transform;

                if (pickupable.gameObject.TryGetComponent<LargeWorldEntity>(out var lwe))
                {
                    lwe.enabled = false;
                }
            }

            SkyEnvironmentChanged.Send(pickupable.gameObject, baseComponent);

            if (pickupable.gameObject.TryGetComponent<Rigidbody>(out var rb))
            {
                UWE.Utils.SetIsKinematicAndUpdateInterpolation(rb, true);
            }

            pickupable.Place();
            return true;
        }
    }
}