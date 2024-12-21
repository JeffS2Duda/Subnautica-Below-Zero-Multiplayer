namespace Subnautica.Network.Models.Items
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class Constructor : NetworkPlayerItemComponent
    {
        [Key(1)]
        public override TechType TechType { get; set; } = TechType.Constructor;

        [Key(4)]
        public ZeroVector3 Forward { get; set; }

        [Key(5)]
        public ZeroVector3 Position { get; set; }

        [Key(6)]
        public WorldDynamicEntity Entity { get; set; }

        [Key(7)]
        public byte EngageToggle { get; set; }

        [Key(8)]
        public TechType CraftingTechType { get; set; }

        [Key(9)]
        public float CraftingFinishTime { get; set; }

        [Key(10)]
        public ZeroVector3 CraftingPosition { get; set; }

        [Key(11)]
        public ZeroQuaternion CraftingRotation { get; set; }

        public bool IsEngageActive()
        {
            return this.EngageToggle != 0;
        }

        public bool IsEngage()
        {
            return this.EngageToggle == 1;
        }
    }
}
