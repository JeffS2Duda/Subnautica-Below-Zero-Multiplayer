namespace Subnautica.Network.Models.Server
{
    using MessagePack;
    
    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class NotificationAddedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.NotificationAdded;

        [Key(5)]
        public NotificationManager.Group Group { get; set; }

        [Key(6)]
        public string Key { get; set; }

        [Key(7)]
        public bool IsNotification { get; set; }

        [Key(8)]
        public bool IsAdded { get; set; }
        
        [Key(9)]
        public bool IsVisible { get; set; } = true;

        [Key(10)]
        public sbyte ColorIndex { get; set; }
    }
}
