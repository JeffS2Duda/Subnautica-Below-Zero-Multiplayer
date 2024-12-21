namespace Subnautica.Client.MonoBehaviours.Entity
{
    using Subnautica.API.Features;
    using Subnautica.Client.MonoBehaviours.Entity.Components;

    using UnityEngine;

    public class MultiplayerEntityTracker : MonoBehaviour
    {
        public EntityInterpolate Interpolate { get; set; } = new EntityInterpolate();

        public EntityPosition Position { get; set; } = new EntityPosition();

        public EntityVisibility Visibility { get; set; } = new EntityVisibility();

        public void Update()
        {
            if (World.IsLoaded)
            {
                this.Visibility.Update();
                this.Position.Update();
                this.Interpolate.Update();
            }
        }
    }
}