namespace Subnautica.Client.Multiplayer.Cinematics
{
    using Subnautica.API.Extensions;
    using Subnautica.Client.MonoBehaviours.Player;
    using System.Collections;
    using UnityEngine;

    public class PrecursorTeleporterTerminalCinematic : CinematicController
    {
        private global::PrecursorTeleporterActivationTerminal Terminal { get; set; }

        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            this.Terminal = this.Target.GetComponent<global::PrecursorTeleporterActivationTerminal>();
            this.Terminal.animator.SetBool("Open", true);
        }

        public override IEnumerator OnResetAnimationsAsync(PlayerCinematicQueueItem item)
        {
            var result = new TaskResult<GameObject>();

            yield return base.OnResetAnimationsAsync(item);
            yield return CraftData.InstantiateFromPrefabAsync(TechType.PrecursorIonCrystal, result);

            this.InitializeIonCube(result.Get());

        }

        public void ActivatePrecursorTerminalCinematic()
        {
            this.Terminal.unlocked = true;

            Utils.PlayFMODAsset(this.Terminal.useSound, this.Terminal.transform);

            this.SetCinematic(this.Terminal.cinematicController);
            this.SetCinematicEndMode(this.ActivateTeleportEndMode);
            this.StartCinematicMode();
        }

        private void InitializeIonCube(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<global::Pickupable>(out var pickupable))
            {
                pickupable.Initialize();

                if (this.Terminal)
                {
                    this.Terminal.unlocked = true;
                    this.Terminal.crystalObject = pickupable.gameObject;
                    this.Terminal.crystalObject.transform.SetParent(this.ZeroPlayer.RightHandItemTransform);
                    this.Terminal.crystalObject.transform.localPosition = Vector3.zero;
                    this.Terminal.crystalObject.transform.localRotation = Quaternion.identity;
                    this.Terminal.crystalObject.SetActive(true);

                    if (this.Terminal.crystalObject.TryGetComponent<Rigidbody>(out var rigidbody))
                    {
                        rigidbody.SetKinematic();
                    }
                }
            }
        }

        private void ActivateTeleportEndMode()
        {
            if (this.Terminal)
            {
                if (this.Terminal.crystalObject)
                {
                    this.Terminal.crystalObject.transform.SetParent(null);
                    this.Terminal.crystalObject.Destroy();
                }

                this.Terminal.CloseDeck();

                if (this.Terminal.root)
                {
                    this.Terminal.root.BroadcastMessage("ToggleDoor", true, SendMessageOptions.RequireReceiver);
                }
                else
                {
                    this.Terminal.BroadcastMessage("ToggleDoor", true, SendMessageOptions.RequireReceiver);
                }
            }
        }
    }
}