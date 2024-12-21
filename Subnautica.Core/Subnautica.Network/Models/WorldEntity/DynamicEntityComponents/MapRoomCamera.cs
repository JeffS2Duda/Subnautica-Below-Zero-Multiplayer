namespace Subnautica.Network.Models.WorldEntity.DynamicEntityComponents
{
    using MessagePack;

    using Subnautica.Network.Core.Components;
    using Subnautica.Network.Models.WorldEntity.DynamicEntityComponents.Shared;

    [MessagePackObject]
    public class MapRoomCamera : NetworkDynamicEntityComponent
    {
        [Key(0)]
        public PowerCell Battery { get; set; }

        [Key(1)]
        public LiveMixin LiveMixin { get; set; }

        [Key(2)]
        public bool IsLightEnabled { get; set; }

        public MapRoomCamera()
        {

        }

        public MapRoomCamera(float charge, float capacity, bool isLightEnabled, float health, float maxHealth)
        {
            this.LiveMixin = new LiveMixin(health, maxHealth);
            this.Battery = new PowerCell();
            this.Battery.Charge = charge;
            this.Battery.Capacity = capacity;
            this.IsLightEnabled = isLightEnabled;
        }
    }
}
