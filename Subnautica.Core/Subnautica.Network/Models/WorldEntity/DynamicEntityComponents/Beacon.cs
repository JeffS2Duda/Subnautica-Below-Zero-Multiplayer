namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Beacon : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public bool IsDeployedOnLand { get; set; }

        [Key(1)]
        public string Text { get; set; }

        public Beacon()
        {

        }

        public Beacon(bool isDeployedOnLand, string text)
        {
            this.IsDeployedOnLand = isDeployedOnLand;
            this.Text             = text;
        }

        public void SetText(string text)
        {
            this.Text = text;
        }
    }
}
