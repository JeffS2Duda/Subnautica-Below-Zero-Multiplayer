namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Flare : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public float Energy { get; set; }

        [Key(1)]
        public float ActivateTime { get; set; }

        public Flare()
        {

        }

        public Flare(float energy, float activateTime)
        {
            this.Energy       = energy;
            this.ActivateTime = activateTime;
        }
    }
}
