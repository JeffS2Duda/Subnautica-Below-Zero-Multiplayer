namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;
    using System.Collections.Generic;

    [MessagePackObject]
    public class BaseMapRoom : MetadataComponent
    {
        [Key(0)]
        public TechType ScanTechType { get; set; }

        [Key(1)]
        public float LastScanDate { get; set; }

        [Key(2)]
        public StorageContainer StorageContainer { get; set; }

        [Key(3)]
        public Crafter Crafter { get; set; }

        [Key(4)]
        public byte ProcessType { get; set; }

        [Key(5)]
        public WorldPickupItem PickupItem { get; set; }

        [Key(6)]
        public HashSet<string> ResourceNodes { get; set; } = new HashSet<string>();

        [Key(7)]
        public VehicleDockingBayItem LeftDock { get; set; } = new VehicleDockingBayItem();

        [Key(8)]
        public VehicleDockingBayItem RightDock { get; set; } = new VehicleDockingBayItem();

        [Key(9)]
        public bool IsNextCamera { get; set; }

        [IgnoreMember]
        public bool IsChanged { get; set; } = false;

        public bool SetLastScanDate(float lastScanDate)
        {
            this.LastScanDate = lastScanDate;
            return true;
        }

        public bool ResetNodes()
        {
            if (this.ResourceNodes.Count <= 0)
            {
                return false;
            }

            this.ResourceNodes.Clear();
            return true;
        }

        public void AddResourceNode(string itemId)
        {
            this.ResourceNodes.Add(itemId);
        }

        public bool IsScanning()
        {
            return this.ScanTechType != TechType.None;
        }

        public bool StartScan(TechType techType)
        {
            if (this.ScanTechType != TechType.None)
            {
                return false;
            }

            this.IsChanged = true;
            this.ScanTechType = techType;
            this.ResetNodes();
            return true;
        }

        public bool StopScan()
        {
            if (this.ScanTechType == TechType.None)
            {
                return false;
            }

            this.IsChanged = true;
            this.ScanTechType = TechType.None;
            this.ResetNodes();
            return true;
        }

        public bool Dock(WorldDynamicEntity vehicle, bool isLeft, ZeroVector3 endPosition, ZeroQuaternion endRotation, float currentTime)
        {
            if (isLeft)
            {
                return this.LeftDock.Dock(vehicle, endPosition, endRotation, currentTime);
            }

            return this.RightDock.Dock(vehicle, endPosition, endRotation, currentTime);
        }

        public bool Undock(string vehicleId, float currentTime, out WorldDynamicEntity vehicle)
        {
            if (this.LeftDock.VehicleId == vehicleId)
            {
                return this.LeftDock.Undock(currentTime, out vehicle);
            }

            if (this.RightDock.VehicleId == vehicleId)
            {
                return this.RightDock.Undock(currentTime, out vehicle);
            }

            vehicle = null;
            return false;
        }
    }

    [MessagePackObject]
    public class VehicleDockingBayItem
    {
        [Key(0)]
        public bool IsDocked { get; set; }

        [Key(1)]
        public float LastDockTime { get; set; }

        [Key(2)]
        public string VehicleId { get; set; }

        [Key(3)]
        public ZeroVector3 Position { get; set; }

        [Key(4)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(5)]
        public WorldDynamicEntity Vehicle { get; set; }

        public VehicleDockingBayItem()
        {

        }

        public VehicleDockingBayItem(bool isDocked, float starTime, string vehicleId, ZeroVector3 position, ZeroQuaternion rotation, WorldDynamicEntity vehicle)
        {
            this.IsDocked = isDocked;
            this.LastDockTime = starTime;
            this.VehicleId = vehicleId;
            this.Position = position;
            this.Rotation = rotation;
            this.Vehicle = vehicle;
        }

        public void SetLastDockTime(float dockTime)
        {
            this.LastDockTime = dockTime;
        }

        public bool Dock(WorldDynamicEntity vehicle, ZeroVector3 endPosition, ZeroQuaternion endRotation, float currentTime)
        {
            if (this.IsDocked)
            {
                return false;
            }

            if (this.LastDockTime + 2f >= currentTime)
            {
                return false;
            }

            this.IsDocked = true;
            this.VehicleId = vehicle.UniqueId;
            this.Vehicle = vehicle;
            this.Vehicle.Position = endPosition;
            this.Vehicle.Rotation = endRotation;
            this.LastDockTime = currentTime;
            return true;
        }

        public bool Undock(float currentTime, out WorldDynamicEntity vehicle)
        {
            vehicle = this.Vehicle;

            if (this.IsDocked)
            {
                this.IsDocked = false;
                this.VehicleId = null;
                this.Vehicle = null;
                this.SetLastDockTime(currentTime);
                return true;
            }

            return false;
        }
    }
}
