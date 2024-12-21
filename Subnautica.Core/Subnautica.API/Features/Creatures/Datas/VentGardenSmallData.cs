namespace Subnautica.API.Features.Creatures.Datas
{
    public class VentGardenSmallData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.SmallVentGarden;

        public override bool IsCanBeAttacked { get; set; } = false;

        public override float Health { get; set; } = 1f;

        public override float VisibilityDistance { get; set; } = 120f;

        public override float VisibilityLongDistance { get; set; } = 150f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 111f;

        public override bool IsRespawnable { get; set; } = false;
    }
}
