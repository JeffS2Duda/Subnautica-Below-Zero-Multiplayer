namespace Subnautica.Client.Synchronizations.Processors.General
{
    using System.Collections.Generic;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class ResourceDiscoverProcessor : NormalProcessor
    {
        private static HashSet<TechType> IgnoreTechs = new HashSet<TechType>();

        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.ResourceDiscoverArgs>();
            if (packet == null)
            {
                return true;
            }

            Network.Session.AddDiscoveredTechType(packet.TechType);

            foreach (var uniqueId in packet.MapRooms)
            {
                var mapRoom = Network.Identifier.GetComponentByGameObject<BaseDeconstructable>(uniqueId)?.GetMapRoomFunctionality();
                if (mapRoom)
                {
                    var scanner = mapRoom.GetComponentInChildren<uGUI_MapRoomScanner>();
                    if (scanner)
                    {
                        UpdateMapRoomScanner(scanner);
                    }
                }
            }

            return true;
        }

        public static void OnBaseMapRoomResourceDiscovering(BaseMapRoomResourceDiscoveringEventArgs ev)
        {
            ev.IsAllowed = false;

            if (ev.TechType != TechType.None && !IgnoreTechs.Contains(ev.TechType))
            {
                ResourceDiscoverProcessor.SendPacketToServer(ev.TechType);
            }
        }

        public static void OnBaseMapRoomInitialized(BaseMapRoomInitializedEventArgs ev)
        {
            UpdateMapRoomScanner(ev.MapRoom);
        }

        private static void UpdateMapRoomScanner(uGUI_MapRoomScanner scanner)
        {
            IgnoreTechs.AddRange(Network.Session.Current.DiscoveredTechTypes);

            scanner.availableTechTypes.Clear();
            scanner.availableTechTypes.AddRange(Network.Session.Current.DiscoveredTechTypes);
            scanner.RebuildResourceList();
        }

        public static void SendPacketToServer(TechType techType)
        {
            IgnoreTechs.Add(techType);

            ServerModel.ResourceDiscoverArgs result = new ServerModel.ResourceDiscoverArgs()
            {
                TechType = techType,
            };

            NetworkClient.SendPacket(result);
        }

        public override void OnStart()
        {
            IgnoreTechs.Add(TechType.HeatArea);
            IgnoreTechs.Add(TechType.Databox);
            IgnoreTechs.Add(TechType.PrecursorIonCrystal);
            IgnoreTechs.Add(TechType.KelpRootPustule);
        }

        public override void OnDispose()
        {
            IgnoreTechs.Clear();
        }
    }
}