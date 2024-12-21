namespace Subnautica.Client.Synchronizations.Processors.Items
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Extensions;
    using Subnautica.Network.Core.Components;

    using ItemModel = Subnautica.Network.Models.Items;

    public class FlashLightProcessor : PlayerItemProcessor
    {
        public override bool OnDataReceived(NetworkPlayerItemComponent packet, byte playerId)
        {
            return true;
        }

        public override void OnFixedUpdate()
        {
            foreach (var player in ZeroPlayer.GetPlayers())
            {
                if (player.TechTypeInHand == TechType.Flashlight)
                {
                    this.ProcessFlashLight(player);
                }
            }
        }

        private bool ProcessFlashLight(ZeroPlayer player)
        {
            if (player.HandItemComponent == null)
            {
                return false;
            }

            var tool = player.GetHandTool<global::FlashLight>(TechType.Flashlight);
            if (tool == null)
            {
                return false;
            }

            var item = player.HandItemComponent.GetComponent<ItemModel.FlashLight>();
            if (item != null)
            {
                ZeroGame.SetLightsActive(tool.toggleLights, item.IsActivated, true);
            }

            return true;
        }
    }
}
