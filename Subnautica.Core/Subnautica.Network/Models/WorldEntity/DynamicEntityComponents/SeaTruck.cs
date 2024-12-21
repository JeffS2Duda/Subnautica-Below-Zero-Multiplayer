namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;
    using Subnautica.API.Features;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;
    using System;
    using System.Collections.Generic;

    [MessagePackObject]
    public class SeaTruck : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public List<UpgradeConsoleItem> Modules { get; set; } = new List<UpgradeConsoleItem>()
        {
            new UpgradeConsoleItem(),
            new UpgradeConsoleItem(),
            new UpgradeConsoleItem(),
            new UpgradeConsoleItem()
        };

        [Key(1)]
        public ZeroColorCustomizer ColorCustomizer { get; set; } = new ZeroColorCustomizer();

        [Key(2)]
        public List<PowerCell> PowerCells { get; set; } = new List<PowerCell>()
        {
            new PowerCell(),
            new PowerCell(),
        };

        [Key(3)]
        public bool IsLightActive { get; set; } = true;

        [Key(4)]
        public LiveMixin LiveMixin { get; set; } = new LiveMixin(500f, 500f);

        public SeaTruck Initialize(Action<NetworkDynamicEntityComponent> onEntityComponentInitialized)
        {

            foreach (var powerCell in this.PowerCells)
            {
                powerCell.UniqueId = Network.Identifier.GenerateUniqueId();
            }

            onEntityComponentInitialized?.Invoke(this);
            return this;
        }
    }
}
