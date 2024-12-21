namespace Subnautica.Network.Models.WorldEntity
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;

#pragma warning disable CS0612

    [MessagePackObject]
    public class Databox : NetworkWorldEntityComponent
    {
        [Key(2)]
        public override EntityProcessType ProcessType { get; set; } = EntityProcessType.Databox;

        [Key(4)]
        public bool IsUsed { get; set; } = true;
    }
}
