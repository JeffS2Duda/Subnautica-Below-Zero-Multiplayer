namespace Subnautica.Client.Synchronizations.Processors.Player
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Metadata;

    using ServerModel = Subnautica.Network.Models.Server;

    public class ToolEnergyProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            return true;
        }

        public static void OnToolBatteryEnergyChanged(ToolBatteryEnergyChangedEventArgs ev)
        {
            if (World.IsLoaded)
            {
                ServerModel.PlayerToolEnergyArgs request = new ServerModel.PlayerToolEnergyArgs()
                {
                    UniqueId = ev.UniqueId,
                    Item = StorageItem.Create(ev.Item)
                };

                NetworkClient.SendPacket(request);
            }
        }
    }
}
