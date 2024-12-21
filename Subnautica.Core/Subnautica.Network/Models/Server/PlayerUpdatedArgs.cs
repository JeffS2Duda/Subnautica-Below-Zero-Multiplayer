namespace Subnautica.Network.Models.Server
{
    using System.Collections.Generic;
    
    using LiteNetLib;

    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class PlayerUpdatedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.PlayerUpdated;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.PlayerMovement;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.Unreliable;

        [Key(5)]
        public short CompressedCameraPitch { get; set; }

        [Key(6)]
        public long CompressedPosition { get; set; }

        [Key(7)]
        public long CompressedRotation { get; set; }


        [Key(8)]
        public long CompressedLocalPosition { get; set; }

        [Key(9)]
        public long CompressedRightHandItemRotation { get; set; }

        [Key(10)]
        public long CompressedLeftHandItemRotation { get; set; }

        [Key(11)]
        public int CompressedCameraForward { get; set; }

        [Key(12)]
        public TechType ItemInHand { get; set; }

        [Key(13)]
        public VFXSurfaceTypes SurfaceType { get; set; }

        [Key(14)]
        public bool IsPrecursorArm { get; set; }

        [Key(15)]
        public byte EmoteIndex { get; set; }
        [Key(16)]
        public List<TechType> Equipments { get; set; }

        [Key(17)]
        public NetworkPlayerItemComponent HandItemComponent { get; set; }
    }
}
