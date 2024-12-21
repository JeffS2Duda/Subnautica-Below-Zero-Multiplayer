namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MessagePack;

    using Subnautica.API.Features;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class Exosuit : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public List<UpgradeConsoleItem> Modules { get; set; } = new List<UpgradeConsoleItem>()
        {
            new UpgradeConsoleItem(),
            new UpgradeConsoleItem(),
            new UpgradeConsoleItem(),
            new UpgradeConsoleItem(),
            new UpgradeConsoleItem(),
            new UpgradeConsoleItem()
        };       

        [Key(1)]
        public List<PowerCell> PowerCells { get; set; } = new List<PowerCell>()
        {
            new PowerCell(),
            new PowerCell(),
        };

        [Key(2)]
        public ZeroColorCustomizer ColorCustomizer { get; set; } = new ZeroColorCustomizer();

        [Key(3)]
        public Metadata.StorageContainer StorageContainer { get; set; }

        [Key(4)]
        public LiveMixin LiveMixin { get; set; } = new LiveMixin(600f, 600f);

        public void ResizeStorageContainer()
        {
            this.StorageContainer.Resize(this.StorageContainer.GetSizeX(), Convert.ToByte(this.Modules.Count(q => q.ModuleType == TechType.VehicleStorageModule) + 4));
        }

        public Exosuit Initialize(Action<NetworkDynamicEntityComponent> onEntityComponentInitialized)
        {
            this.StorageContainer = Metadata.StorageContainer.Create(6, 4);

            foreach (var powerCell in this.PowerCells)
            {
                powerCell.UniqueId = Network.Identifier.GenerateUniqueId();
            }

            onEntityComponentInitialized?.Invoke(this);
            return this;
        }
    }
}
