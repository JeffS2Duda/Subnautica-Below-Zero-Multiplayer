namespace Subnautica.Client.Synchronizations.Processors.Items
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Client.Extensions;
    using Subnautica.Network.Core.Components;

    using ItemModel = Subnautica.Network.Models.Items;

    public class LaserCutterProcessor : PlayerItemProcessor
    {
        public override bool OnDataReceived(NetworkPlayerItemComponent packet, byte playerId)
        {
            return true;
        }

        public override void OnFixedUpdate()
        {
            foreach (var player in ZeroPlayer.GetPlayers())
            {
                if (player.TechTypeInHand == TechType.LaserCutter)
                {
                    this.ProcessLaserCutter(player);
                }
            }
        }

        private bool ProcessLaserCutter(ZeroPlayer player)
        {
            if (player.HandItemComponent == null)
            {
                return false;
            }

            var tool = player.GetHandTool<global::LaserCutter>(TechType.LaserCutter);
            if (tool == null)
            {
                return false;
            }

            var item = player.HandItemComponent.GetComponent<ItemModel.LaserCutter>();
            if (item != null)
            {
                if (item.IsPlaying)
                {
                    tool.StartLaserCuttingFX();
                }
                else
                {
                    tool.StopLaserCuttingFX();
                }
            }

            return true;
        }
    }
}
