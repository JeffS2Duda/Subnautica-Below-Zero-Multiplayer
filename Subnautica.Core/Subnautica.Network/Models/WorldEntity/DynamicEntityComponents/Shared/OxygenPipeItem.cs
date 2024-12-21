namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared
{
    using MessagePack;

    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class OxygenPipeItem
    {
        [Key(0)]
        public string UniqueId { get; set; }

        [Key(1)]
        public string ParentId { get; set; }

        [Key(2)]
        public ZeroVector3 Position { get; set; }
    }
}
