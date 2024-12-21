namespace Subnautica.Network.Models.Storage.Story.Components
{
    using MessagePack;

    [MessagePackObject]
    public class FrozenCreatureComponent
    {
        [Key(0)]
        public bool IsSampleAdded { get; set; }

        [Key(1)]
        public bool IsInjected { get; set; }

        [Key(2)]
        public float InjectTime { get; set; }

        public bool AddSample()
        {
            if (this.IsSampleAdded)
            {
                return false;
            }

            this.IsSampleAdded = true;
            return true;
        }

        public bool Inject(float serverTime)
        {
            if (this.IsInjected)
            {
                return false;
            }

            this.IsInjected = true;
            this.InjectTime = serverTime;
            return true;
        }
    }
}
