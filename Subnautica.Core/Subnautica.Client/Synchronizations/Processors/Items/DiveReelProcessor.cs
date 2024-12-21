namespace Subnautica.Client.Synchronizations.Processors.Items
{
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Network.Core.Components;

    public class DiveReelProcessor : PlayerItemProcessor
    {
        public override bool OnDataReceived(NetworkPlayerItemComponent packet, byte playerId)
        {
            return true;
        }
    }
}
