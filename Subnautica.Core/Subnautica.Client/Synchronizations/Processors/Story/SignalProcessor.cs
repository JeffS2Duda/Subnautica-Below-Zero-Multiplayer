namespace Subnautica.Client.Synchronizations.Processors.Story
{
    using global::Story;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Helper;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using Subnautica.Network.Models.Storage.Story.StoryGoals;
    using Subnautica.Network.Models.Storage.World.Childrens;
    using Subnautica.Network.Structures;

    using UnityEngine;

    using ServerModel = Subnautica.Network.Models.Server;
    using WorldEntityModel = Subnautica.Network.Models.WorldEntity.DynamicEntityComponents;

    public class SignalProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.StorySignalArgs>();
            if (packet.Beacon != null)
            {
                var action = new ItemQueueAction(null, this.OnEntitySpawned);
                action.RegisterProperty("Entity", packet.Beacon);

                Entity.SpawnToQueue(packet.Beacon.TechType, packet.Beacon.UniqueId, new ZeroTransform(packet.Beacon.Position, packet.Beacon.Rotation), action);
            }
            else
            {
                SpawnSignal(packet.Signal);
            }

            return true;
        }

        public static void SpawnSignal(ZeroStorySignal signal)
        {
            switch (signal.SignalType)
            {
                case UnlockSignalData.SignalType.Signal:
                    AddSignal(signal, global::Story.StoryGoalManager.main.onGoalUnlockTracker.signalPrefab, 0);
                    break;
                case UnlockSignalData.SignalType.ArchitectArtifact:
                    AddSignal(signal, global::Story.StoryGoalManager.main.onGoalUnlockTracker.artifactPrefab, 2);
                    break;
            }
        }

        public void OnEntitySpawned(ItemQueueProcess item, Pickupable pickupable, GameObject gameObject)
        {
            var entity = item.Action.GetProperty<WorldDynamicEntity>("Entity");
            if (entity != null)
            {
                Network.DynamicEntity.SetEntity(entity);

                var component = entity.Component.GetComponent<WorldEntityModel.Beacon>();
                if (component != null)
                {
                    WorldEntities.DynamicEntities.BeaconProcessor.InitializeBeaconSignal(pickupable, entity.Position.ToVector3(), entity.Rotation.ToQuaternion(), component.Text, component.IsDeployedOnLand);
                }
            }
        }

        private static void AddSignal(ZeroStorySignal signal, GameObject prefab, int colorIndex)
        {
            var component = Object.Instantiate<GameObject>(prefab).GetComponent<SignalPing>();
            component.pos = signal.TargetPosition.ToVector3();
            component.descriptionKey = signal.TargetDescription;
            component.PlayVO();
            component.pingInstance.SetColor(colorIndex);
            component.pingInstance.AddNotification();
        }

        public static void OnStorySignalSpawning(StorySignalSpawningEventArgs ev)
        {
            ev.IsAllowed = false;

            ServerModel.StorySignalArgs result = new ServerModel.StorySignalArgs()
            {
                Signal = new ZeroStorySignal()
                {
                    SignalType = ev.SignalType,
                    TargetPosition = ev.TargetPosition.ToZeroVector3(),
                    TargetDescription = ev.TargetDescription,
                },
            };

            NetworkClient.SendPacket(result);
        }
    }
}
