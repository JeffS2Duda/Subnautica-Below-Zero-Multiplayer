namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared
{
    using MessagePack;

    [MessagePackObject]
    public class UpgradeConsoleItem
    {
        [Key(0)]
        public string ItemId { get; set; }

        [Key(1)]
        public TechType ModuleType { get; set; }
    }
}
