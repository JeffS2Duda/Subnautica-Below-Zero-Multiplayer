namespace Subnautica.Client.MonoBehaviours.Player
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Helper;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerHandItemManager : MonoBehaviour
    {
        public ZeroPlayer Player { get; set; }

        private TechType ActiveTechType { get; set; } = TechType.None;

        private string ActiveToolName { get; set; } = null;

        private Dictionary<TechType, Pickupable> ItemPool { get; set; } = new Dictionary<TechType, Pickupable>();

        private List<TechType> LoadingItems { get; set; } = new List<TechType>();

        private int DefaultMaskIndex { get; set; } = -1;

        private int ViewMaskIndex { get; set; } = -1;

        private ItemQueueAction QueueAction { get; set; }
        public void Awake()
        {
            this.DefaultMaskIndex = LayerMask.NameToLayer("default");
            this.ViewMaskIndex = LayerMask.NameToLayer("Viewmodel");

            this.QueueAction = new ItemQueueAction();
            this.QueueAction.OnEntitySpawned = this.OnEntitySpawned;
        }

        public void ClearHand()
        {
            this.SetHand(TechType.None);
        }

        public bool SetHand(TechType techType)
        {
            if (this.ActiveTechType == techType)
            {
                return true;
            }

            if (this.ActiveToolName.IsNotNull())
            {
                SafeAnimator.SetBool(this.Player.Animator, string.Format("holding_{0}", this.ActiveToolName), false);
            }

            if (this.ActiveTechType != TechType.None && this.ItemPool.ContainsKey(this.ActiveTechType))
            {
                this.HolsterItem(this.GetItem(this.ActiveTechType));
            }

            if (techType == TechType.PDA)
            {
                this.OpenPda();
                this.OnChangedItem(TechType.PDA);
            }
            else if (this.ActiveTechType == TechType.PDA)
            {
                this.ClosePda();
            }

            if (techType == TechType.None)
            {
                if (this.ActiveTechType == TechType.None)
                {
                    return false;
                }

                this.ActiveToolName = null;
                this.OnChangedItem(TechType.None);
                return true;
            }

            if (techType == TechType.PDA)
            {
                return true;
            }

            if (this.ItemPool.ContainsKey(techType))
            {
                return this.DrawItem(this.GetItem(techType));
            }

            return this.CreateItem(techType);
        }

        public Pickupable GetItem(TechType techType)
        {
            if (this.ItemPool.TryGetValue(techType, out var pickupable))
            {
                return pickupable;
            }

            return null;
        }

        private bool DrawItem(Pickupable pickupable)
        {
            if (pickupable == null)
            {
                return false;
            }

            var tool = pickupable.GetComponent<PlayerTool>();
            if (tool == null)
            {
                return false;
            }

            this.OnChangedItem(pickupable.GetTechType());
            this.ActiveToolName = tool.animToolName;

            ModelPlug.PlugIntoSocket(tool, this.Player.RightHandItemTransform);

            Utils.SetLayerRecursively(tool.gameObject, this.ViewMaskIndex);

            foreach (Animator componentsInChild in tool.GetComponentsInChildren<Animator>())
            {
                componentsInChild.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            }

            if (tool.mainCollider != null)
            {
                tool.mainCollider.enabled = false;
            }

            UWE.Utils.SetIsKinematicAndUpdateInterpolation(tool.GetComponent<Rigidbody>(), true);

            pickupable.DisableColliders();

            tool.gameObject.SetActive(true);

            SafeAnimator.SetBool(this.Player.Animator, string.Format("holding_{0}", this.ActiveToolName), true);

            this.SendEquipmentEvent(pickupable, true);

            this.Player.GetComponent<PlayerLighting>().ApplySkybox();
            return true;
        }

        private bool HolsterItem(Pickupable pickupable)
        {
            var tool = pickupable.GetComponent<PlayerTool>();
            if (tool == null)
            {
                return false;
            }

            tool.gameObject.SetActive(false);

            Utils.SetLayerRecursively(tool.gameObject, this.DefaultMaskIndex);

            if (tool.mainCollider != null)
            {
                tool.mainCollider.enabled = true;
            }

            UWE.Utils.SetIsKinematicAndUpdateInterpolation(tool.GetComponent<Rigidbody>(), false);

            foreach (Animator componentsInChild in tool.GetComponentsInChildren<Animator>())
            {
                componentsInChild.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
            }

            tool.OnHolster();

            this.SendEquipmentEvent(pickupable, false);
            return true;
        }

        private bool CreateItem(TechType techType)
        {
            if (this.LoadingItems.Contains(techType))
            {
                return true;
            }

            this.LoadingItems.Add(techType);

            Entity.SpawnToQueue(techType, Network.Identifier.GenerateUniqueId(), this.QueueAction);
            return true;
        }

        public void OnEntitySpawned(ItemQueueProcess item, Pickupable pickupable, GameObject gameObject)
        {
            if (item.TechType == TechType.SnowBall && pickupable.TryGetComponent<SnowBall>(out var snowBall))
            {
                snowBall.despawnTime = Time.time + (86400 * 7);
            }

            this.LoadingItems.Remove(item.TechType);
            this.ItemPool.Add(item.TechType, pickupable);

            this.SetHand(item.TechType);
        }

        private void OpenPda()
        {
            SafeAnimator.SetBool(this.Player.Animator, "using_pda", true);
            this.Player.LeftHandItemTransform.gameObject.SetActive(true);
        }

        private void ClosePda()
        {
            SafeAnimator.SetBool(this.Player.Animator, "using_pda", false);
            this.Player.LeftHandItemTransform.gameObject.SetActive(false);
        }

        private void OnChangedItem(TechType techType)
        {
            this.ActiveTechType = techType;
            this.Player.TechTypeInHand = techType;
        }

        private void SendEquipmentEvent(Pickupable pickupable, bool status)
        {
            if (pickupable.gameObject.TryGetComponent(out FPModel fpModel))
            {
                fpModel.SetState(status);
            }
        }

        public void OnDestroy()
        {
            foreach (var item in this.ItemPool)
            {
                if (item.Value?.gameObject == null)
                {
                    continue;
                }

                GameObject.Destroy(item.Value.gameObject);
            }

            this.ItemPool.Clear();
            this.LoadingItems.Clear();
        }
    }
}
