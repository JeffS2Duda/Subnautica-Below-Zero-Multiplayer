namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using System.Collections.Generic;

    [MessagePackObject]
    public class WorldCreatureOwnershipChangedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.WorldCreatureOwnershipChanged;

        [Key(5)]
        public List<WorldCreatureOwnershipItem> Creatures { get; set; } = new List<WorldCreatureOwnershipItem>();
    }

    [MessagePackObject]
    public class WorldCreatureOwnershipItem
    {
        [Key(0)]
        public byte OwnerId { get; set; }

        [Key(1)]
        public ushort Id { get; set; }

        [Key(2)]
        public long Position { get; set; }

        [Key(3)]
        public long Rotation { get; set; }

        [Key(4)]
        public TechType TechType { get; set; }

        public WorldCreatureOwnershipItem()
        {

        }

        public WorldCreatureOwnershipItem(byte ownerId, ushort id, long position, long rotation, TechType techType)
        {
            this.OwnerId = ownerId;
            this.Id = id;
            this.Position = position;
            this.Rotation = rotation;
            this.TechType = techType;
        }

        public bool IsExistsOwnership()
        {
            return this.OwnerId != 0;
        }

        public bool IsWaitingRegistation()
        {
            return this.TechType != TechType.None;
        }

        public bool IsPlayDeathAnimation()
        {
            return this.Position == -1;
        }
    }
}
