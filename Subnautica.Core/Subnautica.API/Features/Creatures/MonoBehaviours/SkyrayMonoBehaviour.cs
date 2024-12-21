namespace Subnautica.API.Features.Creatures.MonoBehaviours
{
    using Subnautica.Network.Structures;

    public class SkyrayMonoBehaviour : BaseMultiplayerCreature
    {
        private global::Skyray Skyray { get; set; }

        public void Awake()
        {
            this.Skyray = this.GetComponent<global::Skyray>();
        }

        public void FixedUpdate()
        {
            if (!this.MultiplayerCreature.CreatureItem.IsMine())
            {
                if (this.IsRoosting())
                {
                    this.Skyray.GetAnimator().SetBool("roosting", true);
                    this.Skyray.GetAnimator().SetBool("flapping", false);
                }
                else
                {
                    if (this.Skyray.GetAnimator().GetBool("roosting"))
                    {
                        this.Skyray.GetAnimator().SetBool("roosting", false);
                        this.Skyray.GetAnimator().SetBool("flapping", true);
                    }
                }
            }
        }


        private bool IsRoosting()
        {
            return ZeroVector3.Distance(this.MultiplayerCreature.Creature.leashPosition, this.transform.position) <= 0.03f;
        }
    }
}
