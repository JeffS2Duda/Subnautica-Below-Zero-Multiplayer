namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.API.Features;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class BaseMoonpool : MetadataComponent
    {
        [Key(0)]
        public bool IsDocking { get; set; }

        [Key(1)]
        public bool IsUndocking { get; set; }

        [Key(2)]
        public bool IsUndockingLeft { get; set; }

        [Key(3)]
        public bool IsCustomizerOpening { get; set; }

        [Key(4)]
        public bool IsDocked { get; set; }

        [Key(5)]
        public double DockingStartTime { get; set; }

        [Key(6)]
        public string VehicleId { get; set; }

        [Key(7)]
        public ZeroVector3 EndPosition { get; set; }

        [Key(8)]
        public ZeroQuaternion EndRotation { get; set; }

        [Key(9)]
        public WorldDynamicEntity Vehicle { get; set; }

        [Key(10)]
        public ZeroColorCustomizer ColorCustomizer { get; set; }

        [Key(11)]
        public ZeroVector3 BackModulePosition { get; set; }

        [Key(12)]
        public BaseMoonpoolExpansionManager ExpansionManager { get; set; } = new BaseMoonpoolExpansionManager();

        public bool Dock(WorldDynamicEntity entity, ZeroVector3 endPosition, ZeroQuaternion endRotation, double currentTime)
        {
            if (this.IsDocked)
            {
                return false;
            }
            
            this.IsDocked  = true;
            this.VehicleId = entity.UniqueId;
            this.Vehicle   = entity;
            this.Vehicle.Position = endPosition;
            this.Vehicle.Rotation = endRotation;
            this.DockingStartTime = currentTime;
            return true;
        }

        public bool Undock(out WorldDynamicEntity vehicle)
        {
            vehicle = this.Vehicle;

            if (this.IsDocked)
            {
                this.IsDocked  = false;
                this.VehicleId = null;
                this.Vehicle   = null;
                this.DockingStartTime = 0;
                return true;
            }

            return false;
        }
    }
}
