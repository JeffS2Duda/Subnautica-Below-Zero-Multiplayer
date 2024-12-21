namespace Subnautica.Client.Synchronizations.Processors.PDA
{
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class NotificationProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            return true;
        }

        public static void OnPingVisibilityChanged(PlayerPingVisibilityChangedEventArgs ev)
        {
            NotificationProcessor.SendPacketToServer(ev.UniqueId, isVisible: ev.IsVisible);
        }

        public static void OnPingColorChanged(PlayerPingColorChangedEventArgs ev)
        {
            NotificationProcessor.SendPacketToServer(ev.UniqueId, colorIndex: (sbyte)ev.ColorIndex);
        }

        public static void OnNotificationToggle(NotificationToggleEventArgs ev)
        {
            if (ev.Group != NotificationManager.Group.Gallery && ev.Group != NotificationManager.Group.Inventory)
            {
                NotificationProcessor.SendPacketToServer(ev.Key, ev.Group, ev.IsAdded, true);
            }
        }

        public static void SendPacketToServer(string key, NotificationManager.Group group = default, bool isAdded = false, bool isNotification = false, bool isVisible = false, sbyte colorIndex = -1)
        {
            ServerModel.NotificationAddedArgs result = new ServerModel.NotificationAddedArgs()
            {
                Key = key,
                Group = group,
                IsAdded = isAdded,
                IsNotification = isNotification,
                IsVisible = isVisible,
                ColorIndex = colorIndex,
            };

            NetworkClient.SendPacket(result);
        }
    }
}
