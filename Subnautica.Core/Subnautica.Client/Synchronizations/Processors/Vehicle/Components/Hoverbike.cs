namespace Subnautica.Client.Synchronizations.Processors.Vehicle.Components
{
    using Subnautica.API.Extensions;
    using Subnautica.Network.Models.Server;

    public class Hoverbike
    {
        public static HoverbikeUpdateComponent GetComponent(HoverbikeUpdateComponent component, global::Hoverbike hoverbike)
        {
            component.IsBoosting = hoverbike.boostFxControl.IsPlaying(0);
            component.IsJumping  = hoverbike.jumpFxControl.IsPlaying(0);
            return component;
        }
    }
}
