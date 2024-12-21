namespace Subnautica.Client.MonoBehaviours.Entity.Components
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Core;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using System.Collections.Generic;
    using System.Linq;
    using ServerModel = Subnautica.Network.Models.Server;

    public class EntityPosition
    {
        public StopwatchItem Timing { get; set; } = new StopwatchItem(200f);

        public List<WorldDynamicEntityPosition> Positions { get; set; } = new List<WorldDynamicEntityPosition>();

        public void Update()
        {
            if (this.Timing.IsFinished())
            {
                this.Timing.Restart();

                foreach (var entityId in Network.DynamicEntity.GetActivatedEntityIds())
                {
                    var entity = Network.DynamicEntity.GetEntity(entityId);
                    if (entity == null || entity.IsUsingByPlayer || !entity.IsMine(ZeroPlayer.CurrentPlayer.UniqueId) || entity.ParentId.IsNotNull())
                    {
                        continue;
                    }

                    this.EntityPositionToQueue(entity);
                }

                this.SendPositionPacketToServer();
            }
        }

        private void EntityPositionToQueue(WorldDynamicEntity entity)
        {
            entity.UpdateGameObject();

            if (entity.GameObject)
            {
                entity.Position = entity.GameObject.transform.position.ToZeroVector3();
                entity.Rotation = entity.GameObject.transform.rotation.ToZeroQuaternion();

                this.Positions.Add(new WorldDynamicEntityPosition()
                {
                    Id = entity.Id,
                    Position = entity.Position.Compress(),
                    Rotation = entity.Rotation.Compress(),
                });
            }
        }

        public void SendPositionPacketToServer()
        {
            if (this.Positions.Count > 0)
            {
                foreach (var positions in this.Positions.Split(21))
                {
                    ServerModel.WorldDynamicEntityPositionArgs request = new ServerModel.WorldDynamicEntityPositionArgs()
                    {
                        Positions = positions.ToList(),
                    };

                    NetworkClient.SendPacket(request);
                }

                this.Positions.Clear();
            }
        }
    }
}
