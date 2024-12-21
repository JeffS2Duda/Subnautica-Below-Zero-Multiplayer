namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;
    using System.Collections.Generic;

    [MessagePackObject]
    public class Hoverbike : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public List<UpgradeConsoleItem> Modules { get; set; } = new List<UpgradeConsoleItem>()
        {
            new UpgradeConsoleItem()
        };

        [Key(1)]
        public ZeroColorCustomizer ColorCustomizer { get; set; } = new ZeroColorCustomizer();

        [Key(2)]
        public float Charge { get; set; } = 100f;

        [Key(3)]
        public bool IsLightActive { get; set; } = true;

        [Key(4)]
        public LiveMixin LiveMixin { get; set; } = new LiveMixin(200f, 200f);
    }
}
