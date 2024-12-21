namespace Subnautica.Network.Models.Storage.Player
{
    using MessagePack;

    [MessagePackObject]
    public class NotificationItem
    {
        [Key(0)]
        public NotificationManager.Group Group { get; set; }

        [Key(1)]
        public string Key { get; set; }

        [Key(2)]
        public bool IsViewed { get; set; }

        [Key(3)]
        public bool IsPing { get; set; }

        [Key(4)]
        public bool IsVisible { get; set; }

        [Key(5)]
        public sbyte ColorIndex { get; set; }

        public NotificationItem()
        {

        }

        public NotificationItem(NotificationManager.Group group, string key, bool isViewed, bool isPing, bool isVisible, sbyte colorIndex)
        {
            this.Group      = group;
            this.Key        = key;
            this.IsViewed   = isViewed;
            this.IsPing     = isPing;
            this.IsVisible  = isVisible;
            this.ColorIndex = colorIndex;
        }
    }
}

