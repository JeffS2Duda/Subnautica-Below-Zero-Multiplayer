namespace Subnautica.API.Features.Creatures.Datas;

public class BrinewingData : BaseCreatureData
{
    public override TechType CreatureType { get; set; } = TechType.Brinewing;

    public override bool IsCanBeAttacked { get; set; } = true;

    public override float Health { get; set; } = 100f;

    public override float VisibilityDistance { get; set; } = 65f;

    public override float VisibilityLongDistance { get; set; } = 80f;

    public override float StayAtLeashPositionWhenPassive { get; set; } = 60f;

    public override bool IsRespawnable { get; set; } = true;

    public override int RespawnTimeMin { get; set; } = 300;

    public override int RespawnTimeMax { get; set; } = 300;

    public override void OnRegisterMonoBehaviours(MultiplayerCreature creature)
    {
        base.OnRegisterMonoBehaviours(creature);
    }
}