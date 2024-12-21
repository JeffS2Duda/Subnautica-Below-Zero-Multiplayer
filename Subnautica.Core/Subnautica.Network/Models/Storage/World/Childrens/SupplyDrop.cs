namespace Subnautica.Network.Models.Storage.World.Childrens
{
    using MessagePack;

    using Subnautica.API.Extensions;
    using Subnautica.Network.Structures;

    using UnityEngine;

    [MessagePackObject]
    public class SupplyDrop
    {
        [Key(0)]
        public string UniqueId { get; set; } = null;

        [Key(1)]
        public string FabricatorUniqueId { get; set; } = null;

        [Key(2)]
        public string StorageUniqueId { get; set; } = null;

        [Key(3)]
        public string Key { get; set; } = null;

        [Key(4)]
        public float StartedTime { get; set; } = 0f;

        [Key(5)]
        public sbyte ZoneId { get; set; } = -1;

        [Key(6)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(7)]
        public Metadata.StorageContainer StorageContainer { get; set; }

        public void SetConfiguration(float startedTime)
        {
            this.StartedTime = startedTime;
            this.ZoneId = (sbyte)Random.Range(0, 3);
        }

        public void SetKey(string key)
        {
            this.Key = key;
        }

        public void Initialize()
        {
            this.FabricatorUniqueId = API.Features.Network.Identifier.GenerateUniqueId();
            this.StorageUniqueId = API.Features.Network.Identifier.GenerateUniqueId();
            this.UniqueId = API.Features.Network.Identifier.GenerateUniqueId();
            this.Rotation = Quaternion.Euler(0.0f, Random.Range(0, 360), 0.0f).ToZeroQuaternion();
        }

        public bool IsCompleted(float currentTime)
        {
            return this.StartedTime != 0 && currentTime > this.StartedTime + 32f;
        }
    }
}
