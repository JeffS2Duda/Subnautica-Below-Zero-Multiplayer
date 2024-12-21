namespace Subnautica.Client.MonoBehaviours.Entity.Components
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    public class EntityVisibility
    {
        private StopwatchItem Timing { get; set; } = new StopwatchItem(250f);

        public ZeroVector3 PlayerPosition { get; set; }

        public string PlayerUniqueId { get; set; }

        public void Update()
        {
            if (this.Timing.IsFinished())
            {
                this.Timing.Restart();

                this.LoadPlayerData();

                foreach (var entity in Network.DynamicEntity.GetEntities())
                {
                    entity.Value.CacheKinematicStatus();

                    this.ToggleChangeEntityVisibility(entity.Value);
                    this.ToggleChangeEntityPhysicsState(entity.Value);
                }
            }
        }

        public bool ToggleChangeEntity(string uniqueId)
        {
            var entity = Network.DynamicEntity.GetEntity(uniqueId);
            if (entity != null)
            {
                entity.CacheKinematicStatus();

                this.LoadPlayerData();
                this.ToggleChangeEntityVisibility(entity);
                this.ToggleChangeEntityPhysicsState(entity);
                return true;
            }

            return false;
        }

        private void ToggleChangeEntityVisibility(WorldDynamicEntity entity)
        {
            if (this.IsGlobalEntity(entity.TechType))
            {
                if (!Network.DynamicEntity.IsEntityActivated(entity.Id))
                {
                    this.ChangeEntityVisibility(entity, true);
                }
            }
            else
            {
                var isVisible = entity.IsVisible(this.PlayerPosition);
                if (isVisible)
                {
                    if (!Network.DynamicEntity.IsEntityActivated(entity.Id))
                    {
                        this.ChangeEntityVisibility(entity, true);
                    }
                }
                else
                {
                    if (Network.DynamicEntity.IsEntityActivated(entity.Id))
                    {
                        this.ChangeEntityVisibility(entity, false);
                    }
                }
            }
        }

        private bool ToggleChangeEntityPhysicsState(WorldDynamicEntity entity)
        {
            var kinematicState = Network.DynamicEntity.CalculateKinematic(entity, this.PlayerPosition, this.PlayerUniqueId);
            if (kinematicState == entity.KinematicState || kinematicState == ZeroKinematicState.Ignore)
            {
                return false;
            }

            return Network.DynamicEntity.ToggleKinematic(entity, kinematicState);
        }

        private void ChangeEntityVisibility(WorldDynamicEntity entity, bool isActivated)
        {
            entity.UpdateGameObject();

            if (entity.GameObject)
            {
                entity.GameObject.SetActive(isActivated);

                if (isActivated)
                {
                    Network.DynamicEntity.ToggleKinematic(entity, Network.DynamicEntity.CalculateKinematic(entity, this.PlayerPosition, this.PlayerUniqueId));
                    Network.DynamicEntity.ActivateEntity(entity.Id);
                }
                else
                {
                    Network.DynamicEntity.ToggleKinematic(entity, ZeroKinematicState.Kinematic);
                    Network.DynamicEntity.DisableEntity(entity.Id);
                }
            }
        }

        private bool IsGlobalEntity(TechType techType)
        {
            return TechGroup.GlobalEntityTypes.Contains(techType);
        }

        private void LoadPlayerData()
        {
            this.PlayerPosition = global::Player.main.transform.position.ToZeroVector3();
            this.PlayerUniqueId = ZeroPlayer.CurrentPlayer.UniqueId;
        }
    }
}
