namespace Subnautica.Network.Models.Storage.Story.Components
{
    using MessagePack;

    using Subnautica.Network.Structures;

    [MessagePackObject]
    public class CustomDoorwayComponent
    {
        [Key(0)]
        public string UniqueId { get; set; }

        [Key(1)]
        public ZeroVector3 Position { get; set; }

        [Key(2)]
        public ZeroQuaternion Rotation { get; set; }

        [Key(3)]
        public ZeroVector3 Scale { get; set; }

        [Key(4)]
        public bool IsActive { get; set; } = true;

        public CustomDoorwayComponent()
        {

        }

        public CustomDoorwayComponent(string uniqueId, ZeroVector3 position, ZeroQuaternion rotation, ZeroVector3 scale, bool isActive)
        {
            this.UniqueId = uniqueId;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale    = scale;
            this.IsActive = isActive;
        }
    }
}
