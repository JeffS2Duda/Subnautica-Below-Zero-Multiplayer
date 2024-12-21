namespace Subnautica.Network.Models.Creatures
{
    using MessagePack;

    using Subnautica.API.Features;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features.Creatures.Datas;
    using Subnautica.Network.Structures;
    using Subnautica.Network.Models.Server;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Models.Core;
    using Subnautica.API.Enums;

    [MessagePackObject]
    public class MultiplayerCreatureItem
    {
        [Key(0)]
        public byte OwnerId { get; set; }

        [Key(1)]
        public ushort Id { get; set; }

        [Key(2)]
        public ZeroVector3 LeashPosition { get; set; }

        [Key(3)]
        public ZeroQuaternion LeashRotation { get; set; }

        [Key(4)]
        public TechType TechType { get; set; }

        [Key(5)]
        public LiveMixin LiveMixin { get; set; }

        [IgnoreMember]
        public ZeroVector3 Position { get; private set; }

        [IgnoreMember]
        public ZeroQuaternion Rotation { get; private set; }

        [IgnoreMember]
        public ZeroInt3 CellId { get; private set; }

        [IgnoreMember]
        public string WorldStreamerId { get; private set; }

        [IgnoreMember]
        public byte BusyOwnerId { get; private set; }

        [IgnoreMember]
        private bool IsCreatureBusy { get; set; }

        [IgnoreMember]
        private NetworkPacket ActionPacket { get; set; }

        [IgnoreMember]
        public BaseCreatureData Data { get; private set; }

        public MultiplayerCreatureItem()
        {

        }

        public MultiplayerCreatureItem(byte ownerId, ushort id, ZeroVector3 position, ZeroQuaternion rotation, TechType techType)
        {
            this.OwnerId       = ownerId;
            this.Id            = id;
            this.LeashPosition = position;
            this.LeashRotation = rotation;
            this.TechType      = techType;
            this.Data          = this.TechType.GetCreatureData();
            this.LiveMixin     = new LiveMixin(this.Data.Health, this.Data.Health);
        }

        public void SetOwnership(byte ownershipId)
        {
            this.OwnerId = ownershipId;
        }

        public void SetWorldStreamerId(string worldStreamerId)
        {
            this.WorldStreamerId = worldStreamerId;
        }

        public void SetAction(NetworkPacket lastAction, byte busyOwnerId = 0)
        {
            this.ActionPacket = lastAction;

            if (busyOwnerId != 0)
            {
                this.SetBusyOwnerId(busyOwnerId);
            }
        }

        public void SetBusyOwnerId(byte ownerId)
        {
            this.BusyOwnerId = ownerId;
        }

        public void SetBusy(bool isBusy)
        {
            this.IsCreatureBusy = isBusy;
        }

        public void ClearBusyOwnerId()
        {
            this.BusyOwnerId = 0;
        }

        public void ClearAction(bool clearBusyOwner)
        {
            this.ActionPacket = null;

            if (clearBusyOwner)
            {
                this.ClearBusyOwnerId();
            }
        }

        public void SetPositionAndRotation(ZeroVector3 position, ZeroQuaternion rotation)
        {
            this.SetPosition(position);
            this.SetRotation(rotation);
        }

        public void SetPosition(ZeroVector3 position)
        {
            this.Position = position;
        }

        public void SetRotation(ZeroQuaternion rotation)
        {
            this.Rotation = rotation;
        }

        public void SetCellId(ZeroInt3 cellId)
        {
            this.CellId = cellId;
        }
        
        public NetworkPacket GetAction()
        {
            return this.ActionPacket;
        }
        
        public ProcessType GetActionType()
        {
            return this.ActionPacket != null ? this.ActionPacket.Type : ProcessType.None;
        }

        public bool IsActionExists()
        {
            return this.GetAction() != null;
        }

        public bool IsBusy()
        {
            return this.BusyOwnerId != 0 || this.IsCreatureBusy;
        }

        public bool IsFrozen()
        {
            return this.GetActionType() is CreatureFreezeArgs;
        }

        public bool IsNotMine(byte ownershipId = 0)
        {
            return !this.IsMine(ownershipId);
        }

        public bool IsMine(byte ownershipId = 0)
        {
            if (ownershipId == 0)
            {
                return this.OwnerId == ZeroPlayer.CurrentPlayer.PlayerId;
            }

            return this.OwnerId == ownershipId;
        }

        public bool IsExistsOwnership()
        {
            return this.OwnerId != 0;
        }
    }
}
