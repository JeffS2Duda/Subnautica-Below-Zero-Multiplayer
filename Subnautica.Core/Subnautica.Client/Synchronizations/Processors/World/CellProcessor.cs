namespace Subnautica.Client.Synchronizations.Processors.World
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    public class CellProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            return true;
        }

        public static void OnCellLoading(CellLoadingEventArgs ev)
        {
            Network.CellManager.SetLoaded(ev.BatchId, ev.CellId, true);
        }

        public static void OnCellUnLoading(CellUnLoadingEventArgs ev)
        {
            Network.CellManager.SetLoaded(ev.BatchId, ev.CellId, false);
        }
    }
}
