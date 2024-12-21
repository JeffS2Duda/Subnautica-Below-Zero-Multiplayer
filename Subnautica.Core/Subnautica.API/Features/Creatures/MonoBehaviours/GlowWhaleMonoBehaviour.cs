namespace Subnautica.API.Features.Creatures.MonoBehaviours
{ 
    public class GlowWhaleMonoBehaviour : BaseMultiplayerCreature
    {
        private global::GlowWhale GlowWhale { get; set; }

        public void Awake()
        {
            this.GlowWhale = this.GetComponent<global::GlowWhale>();
        }

        public void Update()
        {
            if (this.MultiplayerCreature.CreatureItem.IsNotMine())
            {
                this.GlowWhale.Update();
            }
        }
    }
}
