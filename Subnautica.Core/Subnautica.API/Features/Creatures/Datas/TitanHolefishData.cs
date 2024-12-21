namespace Subnautica.API.Features.Creatures.Datas
{
    public class TitanHolefishData : BaseCreatureData
    {
        public override TechType CreatureType { get; set; } = TechType.TitanHolefish;

        public override bool IsCanBeAttacked { get; set; } = true;

        public override float Health { get; set; } = 2000f;

        public override float VisibilityDistance { get; set; } = 90f;

        public override float VisibilityLongDistance { get; set; } = 110f;

        public override float StayAtLeashPositionWhenPassive { get; set; } = 70f;

        public override bool IsRespawnable { get; set; } = true;

        public override int RespawnTimeMin { get; set; } = 600;

        public override int RespawnTimeMax { get; set; } = 600;
    }
}
