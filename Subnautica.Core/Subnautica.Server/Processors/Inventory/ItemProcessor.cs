namespace Subnautica.Server.Processors.Inventory
{
    using Server.Core;
    
    using Subnautica.Network.Models.Core;
    using Subnautica.Server.Abstracts.Processors;

    using ServerModel = Subnautica.Network.Models.Server;

    public class ItemProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.InventoryItemArgs>();
            if (packet == null)
            {
                return this.SendEmptyPacketErrorLog(networkPacket);
            }

            if (packet.IsAdded)
            {
                profile.AddInventoryItem(packet.Item);
            }
            else
            {
                profile.RemoveInventoryItem(packet.ItemId);
            }

            return true;
        }
    }
}
