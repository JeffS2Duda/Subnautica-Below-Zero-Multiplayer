namespace Subnautica.Network.Models.Storage.Story.Components
{
    using MessagePack;

    [MessagePackObject]
    public class ShieldBaseComponent
    {
        [Key(0)]
        public bool IsFirstEntered { get; set; }

        public bool Enter()
        {
            if (this.IsFirstEntered)
            {
                return false;
            }

            this.IsFirstEntered = true;
            return true;
        }
    }
}
