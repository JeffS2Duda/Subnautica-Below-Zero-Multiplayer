namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class Crafter : MetadataComponent
    {
        [Key(0)]
        public bool IsOpened { get; set; }

        [Key(1)]
        public bool IsPickup { get; set; }

        [Key(2)]
        public TechType CraftingTechType { get; set; }

        [Key(3)]
        public float CraftingDuration { get; set; }

        [Key(4)]
        public float CraftingStartTime { get; set; }

        [Key(5)]
        public Crafter CrafterClone { get; set; }

        public bool Open()
        {
            this.IsOpened = true;
            return true;
        }

        public bool Close()
        {
            this.IsOpened = false;
            return true;
        }

        public bool Craft(TechType techType, float startTime, float duration)
        {
            if (this.CraftingTechType != TechType.None)
            {
                return false;
            }

            this.CraftingTechType  = techType;
            this.CraftingStartTime = startTime;
            this.CraftingDuration  = duration;
            return true;
        }

        public bool TryPickup()
        {
            if (this.CraftingTechType == TechType.None)
            {
                return false;
            }

            this.CraftingTechType  = TechType.None;
            this.CraftingStartTime = 0f;
            this.CraftingDuration  = 0f;
            return true;
        }
    }
}
