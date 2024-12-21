namespace Subnautica.Network.Models.Metadata
{
    using MessagePack;

    using Subnautica.API.Extensions;
    using Subnautica.Network.Core.Components;

    [MessagePackObject]
    public class BaseMoonpoolExpansionManager : MetadataComponent
    {
        [Key(0)]
        public string TailId { get; set; }

        public void DockTail(string tailId)
        {
            this.TailId = tailId;
        }

        public void UndockTail()
        {
            this.TailId = null;
        }

        public bool IsTailDocked()
        {
            return this.TailId.IsNotNull();
        }
    }
}
