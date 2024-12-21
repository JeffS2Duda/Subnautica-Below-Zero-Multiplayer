namespace Subnautica.Network.Models.Storage.Construction
{
    using System;

    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Structures;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;


    [MessagePackObject]
    public class ConstructionItem
    {
        [Key(0)]
        public uint Id { get; set; }

        [Key(1)]
        public string UniqueId { get; set; }

        [Key(2)]
        public string BaseId { get; set; }

        [Key(3)]
        public TechType TechType { get; set; }

        [Key(4)]
        public int LastRotation { get; set; }

        [Key(5)]
        public ZeroVector3 PlacePosition { get; set; }

        [Key(6)]
        public ZeroVector3 CellPosition { get; set; }

        [Key(7)]
        public ZeroVector3 LocalPosition { get; set; }

        [Key(8)]
        public ZeroQuaternion LocalRotation { get; set; }

        [Key(9)]
        public bool IsFaceHasValue { get; set; }

        [Key(10)]
        public Base.Direction FaceDirection { get; set; }

        [Key(11)]
        public Base.FaceType FaceType { get; set; }

        [Key(12)]
        public float ConstructedAmount { get; set; }

        [Key(13)]
        public bool IsBasePiece { get; set; }

        [Key(14)]
        public bool IsStatic { get; set; }

        [Key(15)]
        public MetadataComponent Component { get; set; }

        [Key(16)]
        public LiveMixin LiveMixin { get; set; }

        public bool IsConstructed()
        {
            return this.ConstructedAmount >= 1f;
        }

        public static ConstructionItem CreateStaticItem(string uniqueId, TechType techType)
        {
            return new ConstructionItem()
            {
                IsStatic          = true,
                UniqueId          = uniqueId,
                TechType          = techType,
                ConstructedAmount = 1f,
            };
        }

        public T EnsureComponent<T>()
        {
            if(this.Component == null)
            {
                this.Component = (MetadataComponent) Activator.CreateInstance(typeof(T));
            }

            return this.Component.GetComponent<T>();
        }
    }
}
