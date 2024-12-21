namespace Subnautica.Server.Processors.Items
{
    using Subnautica.Network.Models.Server;
    using Subnautica.Server.Abstracts.Processors;
    using Subnautica.Server.Core;

    internal class ScannerProcessor : PlayerItemProcessor
    {
        public override bool OnDataReceived(AuthorizationProfile profile, PlayerItemActionArgs packet)
        {
            profile.SendPacketToOtherClients(packet);
            return true;
        }
    }
}
