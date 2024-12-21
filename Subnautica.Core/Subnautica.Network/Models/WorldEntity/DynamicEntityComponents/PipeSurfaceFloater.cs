namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;
    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;
    using Subnautica.Network.Structures;
    using System.Collections.Generic;
    using System.Linq;

    [MessagePackObject]
    public class PipeSurfaceFloater : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public HashSet<OxygenPipeItem> Childrens { get; set; } = new HashSet<OxygenPipeItem>();

        public bool AddOxygenPipe(string pipeId, string parentId, ZeroVector3 position)
        {
            if (this.Childrens.Any(q => q.UniqueId == pipeId))
            {
                return false;
            }

            return this.Childrens.Add(new OxygenPipeItem()
            {
                UniqueId = pipeId,
                ParentId = parentId,
                Position = position,
            });
        }
    }
}
