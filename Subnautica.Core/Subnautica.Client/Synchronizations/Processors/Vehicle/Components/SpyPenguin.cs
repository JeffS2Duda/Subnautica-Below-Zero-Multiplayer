namespace Subnautica.Client.Synchronizations.Processors.Vehicle.Components
{
    using Subnautica.Network.Models.Server;
    using System.Collections.Generic;

    public class SpyPenguin
    {
        public static SpyPenguinUpdateComponent GetComponent(SpyPenguinUpdateComponent component, global::SpyPenguin spyPenguin, List<string> animations)
        {
            component.IsSelfieMode = spyPenguin.selfieMode;
            component.SelfieNumber = global::Player.main.playerAnimator.GetBool("selfies") ? global::Player.main.playerAnimator.GetFloat("selfie_number") : -1;
            component.Animations = animations;
            return component;
        }
    }
}
