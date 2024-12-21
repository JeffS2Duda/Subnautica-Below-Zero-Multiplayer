namespace Subnautica.API.Features.Creatures.MonoBehaviours.Shared
{
    public class MultiplayerCreaturedShared : BaseMultiplayerCreature
    {
        public global::CreatureFrozenMixin FrozenMixin { get; private set; }

        public void Awake()
        {
            this.FrozenMixin = this.GetComponent<global::CreatureFrozenMixin>();
        }

        public void OnChangedOwnership()
        {
            if (this.FrozenMixin != null && this.FrozenMixin.IsFrozen() && this.MultiplayerCreature.CreatureItem.IsMine())
            {
                this.FrozenMixin.FreezeForTime(4f);
            }
        }
    }
}
