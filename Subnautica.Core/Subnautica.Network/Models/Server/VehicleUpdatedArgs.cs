namespace Subnautica.Network.Models.Server
{
    using LiteNetLib;
    using MessagePack;
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Structures;
    using System;
    using System.Collections.Generic;

    [MessagePackObject]
    public class VehicleUpdatedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.VehicleUpdated;

        [Key(1)]
        public override NetworkChannel ChannelType { get; set; } = NetworkChannel.VehicleMovement;

        [Key(2)]
        public override DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.Unreliable;

        [Key(5)]
        public byte PlayerId { get; set; }

        [Key(6)]
        public ushort EntityId { get; set; }

        [Key(7)]
        public ZeroVector3 Position { get; set; }

        [Key(8)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(9)]
        public VehicleUpdateComponent Component { get; set; }
    }

    [Union(0, typeof(ExosuitUpdateComponent))]
    [Union(1, typeof(SpyPenguinUpdateComponent))]
    [Union(2, typeof(HoverbikeUpdateComponent))]
    [MessagePackObject]
    public abstract class VehicleUpdateComponent
    {
        [IgnoreMember]
        public bool IsNew { get; set; }

        public T GetComponent<T>()
        {
            if (this is T)
            {
                return (T)Convert.ChangeType(this, typeof(T));
            }

            return default(T);
        }
    }

    [Union(0, typeof(ExosuitDrillArmComponent))]
    [Union(1, typeof(ExosuitGrapplingArmComponent))]
    [Union(2, typeof(ExosuitClawArmComponent))]
    [MessagePackObject]
    public abstract class ExosuitArmComponent
    {
        public T GetComponent<T>()
        {
            if (this is T)
            {
                return (T)Convert.ChangeType(this, typeof(T));
            }

            return default(T);
        }
    }

    [MessagePackObject]
    public class ExosuitUpdateComponent : VehicleUpdateComponent
    {
        [Key(0)]
        public bool IsOnGround { get; set; }

        [Key(1)]
        public ZeroVector3 CameraPosition { get; set; }

        [Key(2)]
        public float AngleX { get; set; }

        [Key(3)]
        public bool IsPlayingJumpSound { get; set; }

        [Key(4)]
        public bool IsPlayingBoostSound { get; set; }

        [Key(5)]
        public ExosuitArmComponent LeftArm { get; set; }

        [Key(6)]
        public ExosuitArmComponent RightArm { get; set; }
    }

    [MessagePackObject]
    public class SpyPenguinUpdateComponent : VehicleUpdateComponent
    {
        [Key(0)]
        public bool IsSelfieMode { get; set; }

        [Key(1)]
        public float SelfieNumber { get; set; }

        [Key(2)]
        public List<string> Animations { get; set; }
    }

    [MessagePackObject]
    public class HoverbikeUpdateComponent : VehicleUpdateComponent
    {
        [Key(0)]
        public bool IsJumping { get; set; }

        [Key(1)]
        public bool IsBoosting { get; set; }
    }

    [MessagePackObject]
    public class ExosuitDrillArmComponent : ExosuitArmComponent
    {
        [Key(0)]
        public bool IsDrilling { get; set; }

        [Key(1)]
        public bool IsFxPlaying { get; set; }
    }

    [MessagePackObject]
    public class ExosuitClawArmComponent : ExosuitArmComponent
    {
        [Key(0)]
        public bool IsBash { get; set; }

        [Key(1)]
        public bool IsPickup { get; set; }

        [Key(2)]
        public bool IsUsing { get; set; }
    }

    [MessagePackObject]
    public class ExosuitGrapplingArmComponent : ExosuitArmComponent
    {
        [Key(0)]
        public ZeroVector3 HookPosition { get; set; }

        [Key(1)]
        public ZeroQuaternion HookRotation { get; set; }

        [Key(2)]
        public bool IsFlying { get; set; }

        [Key(3)]
        public bool IsAttached { get; set; }

        [Key(4)]
        public bool IsUsing { get; set; }

        [Key(5)]
        public bool IsStopped { get; set; }
    }
}
