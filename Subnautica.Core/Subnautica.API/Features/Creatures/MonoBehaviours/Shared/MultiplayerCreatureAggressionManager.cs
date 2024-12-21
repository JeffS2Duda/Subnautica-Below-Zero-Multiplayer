namespace Subnautica.API.Features.Creatures.MonoBehaviours.Shared
{
    public class MultiplayerCreatureAggressionManager : BaseMultiplayerCreature
    {
        public global::CreatureAggressionManager CreatureAggressionManager { get; private set; }

        public void Awake()
        {
            this.CreatureAggressionManager = this.GetComponent<global::CreatureAggressionManager>();
        }

        public void OnEnable()
        {
            this.OnChangedOwnership();
        }

        public void OnChangedOwnership()
        {
            this.CancelInvokes();

            if (this.MultiplayerCreature.CreatureItem.IsMine())
            {
                this.CreatureAggressionManager.EnableAggressionToFish();
                this.CreatureAggressionManager.EnableAggressionToSharks();
            }
        }

        private void CancelInvokes()
        {
            if (this.CreatureAggressionManager.aggressionToSharksPaused)
            {
                this.CreatureAggressionManager.CancelInvoke("EnableAggressionToSharks");
            }

            if (this.CreatureAggressionManager.aggressionToFishPaused)
            {
                this.CreatureAggressionManager.CancelInvoke("EnableAggressionToFish");
            }
        }
    }
}
