namespace Subnautica.Network.Models.WorldEntity
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class SealedObject : NetworkWorldEntityComponent
    {
        [Key(2)]
        public override EntityProcessType ProcessType { get; set; } = EntityProcessType.SealedObject;

        [Key(4)]
        public bool IsSealed { get; set; }

        [Key(5)]
        public float Amount { get; set; }

        [Key(6)]
        public float MaxAmount { get; set; }
    }
}
