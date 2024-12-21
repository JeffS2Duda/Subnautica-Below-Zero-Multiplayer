namespace Subnautica.Client.Multiplayer.Cinematics
{
    using Subnautica.Client.MonoBehaviours.Player;

    public class RadioTowerCinematic : CinematicController
    {
        private global::RadioTowerController RadioTower { get; set; }


        public override void OnResetAnimations(PlayerCinematicQueueItem item)
        {
            this.RadioTower = this.Target.GetComponent<global::RadioTowerController>();
            this.RadioTower.insertedItemCinematicController.animator.Rebind();
        }

        public void InsertedItemStartCinematic()
        {
            this.RadioTower.insertedItem.SetActive(true);
            this.RadioTower.insertedItemParent = this.RadioTower.insertedItem.transform.parent;

            ModelPlug.PlugIntoSocket(this.RadioTower.insertedItem.transform, null, this.ZeroPlayer.RightHandItemTransform);

            this.SetCinematic(this.RadioTower.insertedItemCinematicController);
            this.StartCinematicMode();
        }
    }
}
