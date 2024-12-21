namespace Subnautica.Client.MonoBehaviours.General
{
    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.MonoBehaviours.Creature;
    using Subnautica.Client.MonoBehaviours.Entity;
    using Subnautica.Network.Models.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UWE;

    public class MultiplayerChannelProcessor : MonoBehaviour
    {
        public Queue<NetworkPacket> Packets { get; set; } = new Queue<NetworkPacket>();

        public NetworkChannel ChannelId { get; set; }

        public bool IsActive { get; set; } = false;

        private bool IsAsyncProcessor { get; set; } = false;

        public bool IsAsyncConsuming { get; set; } = false;

        public static Dictionary<NetworkChannel, MultiplayerChannelProcessor> Processors { get; set; } = new Dictionary<NetworkChannel, MultiplayerChannelProcessor>();

        public void Start()
        {
            if (!Processors.TryGetValue(this.ChannelId, out var instance))
            {
                Processors.Add(this.ChannelId, this);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Update()
        {
            if (!this.IsActive)
            {
                return;
            }

            if (this.IsAsyncProcessor)
            {
                if (!this.IsAsyncConsuming && this.Packets.Count > 0)
                {
                    CoroutineHost.StartCoroutine(this.AsyncConsumeQueue());
                }
            }
            else
            {
                while (this.Packets.Count > 0)
                {
                    try
                    {
                        var packet = this.Packets.Dequeue();
                        if (packet != null)
                        {
                            if (ProcessorShared.Processors.TryGetValue(packet.Type, out NormalProcessor processor))
                            {
                                processor.SetFinished(true);
                                processor.OnDataReceived(packet);
                            }
                            else
                            {
                                Log.Error($"Processor Not Found: " + packet.Type);
                            }
                        }
                        else
                        {
                            Log.Error($"Packet Not Found: " + this.ChannelId);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error($"MultiplayerDataReceive Exception, ChannelId: {this.ChannelId}, Error Message: {e}");
                    }
                }
            }
        }

        public IEnumerator AsyncConsumeQueue()
        {
            this.IsAsyncConsuming = true;

            while (this.Packets.Count > 0)
            {
                var packet = this.Packets.Dequeue();
                if (packet != null)
                {
                    if (ProcessorShared.Processors.TryGetValue(packet.Type, out NormalProcessor processor))
                    {
                        try
                        {
                            processor.SetFinished(true);
                            processor.OnDataReceived(packet);
                        }
                        catch (Exception e)
                        {
                            processor.SetFinished(true);
                            Log.Error($"AsyncConsumeQueue Exception, ChannelId: {this.ChannelId}, Error Message: {e}");
                        }

                        while (!processor.IsFinished())
                        {
                            yield return CoroutineUtils.waitForNextFrame;
                        }

                        if (processor.IsWaitingForNextFrame())
                        {
                            yield return CoroutineUtils.waitForNextFrame;
                        }
                    }
                    else
                    {
                        Log.Error($"AsyncConsumeQueue -> Processor Not Found: {packet.Type}");
                    }
                }
                else
                {
                    Log.Error($"AsyncConsumeQueue -> Packet Not Found: {this.ChannelId}");
                }
            }

            this.IsAsyncConsuming = false;
        }

        public void SetEnabled(bool isActive)
        {
            this.IsActive = isActive;
        }

        public void SetChannel(NetworkChannel channelId)
        {
            this.ChannelId = channelId;
        }

        public void SetAsyncEnabled(bool isAsyncProcessor)
        {
            this.IsAsyncProcessor = isAsyncProcessor;
        }

        public void AddPacket(NetworkPacket packet)
        {
            this.Packets.Enqueue(packet);
        }

        public void ClearPackets()
        {
            this.Packets.Clear();
        }

        public void OnDestroy()
        {
            Processors.Remove(this.ChannelId);
        }

        public static void AddPacketToProcessor(NetworkChannel channelId, NetworkPacket packet)
        {
            if (Processors.TryGetValue(channelId, out var processor))
            {
                processor.AddPacket(packet);
            }
            else
            {
                Log.Error(string.Format("[MultiplayerChannelProcessor.AddPacketToProcessor] Channel Not Found: {0}", channelId));
            }
        }

        public static bool AddToPlayerMultiplayerProcessors()
        {
            MultiplayerChannelProcessor.Processors.Clear();

            ZeroPlayer.DisposeAll();

            var player = new ZeroPlayer("Server Host && Client", true);
            player.PlayerObject.AddComponent<ConnectionSignals>();
            player.PlayerObject.AddComponent<MultiplayerEntityTracker>();
            player.PlayerObject.AddComponent<CreatureWatcher>();

            ProcessorBehaviour processorBehaviour = player.PlayerObject.AddComponent<ProcessorBehaviour>();

            AddProcessorToBehaviour(processorBehaviour, ProcessorShared.Processors.Values);
            AddProcessorToBehaviour(processorBehaviour, ProcessorShared.MetadataProcessors.Values);
            AddProcessorToBehaviour(processorBehaviour, ProcessorShared.PlayerItemProcessors.Values);
            AddProcessorToBehaviour(processorBehaviour, ProcessorShared.WorldEntityProcessors.Values);
            AddProcessorToBehaviour(processorBehaviour, ProcessorShared.WorldCreatureProcessors.Values);
            AddProcessorToBehaviour(processorBehaviour, ProcessorShared.WorldDynamicEntityProcessors.Values);

            foreach (NetworkChannel channelId in Enum.GetValues(typeof(NetworkChannel)))
            {
                var channelProcessor = ZeroPlayer.CurrentPlayer.PlayerObject.AddComponent<MultiplayerChannelProcessor>();
                channelProcessor.SetChannel(channelId);

                if (channelId == NetworkChannel.Startup)
                {
                    channelProcessor.SetEnabled(true);
                }
            }

            return true;
        }

        public static void AddProcessorToBehaviour(ProcessorBehaviour behaviour, IEnumerable<BaseProcessor> processors)
        {
            foreach (var processor in processors)
            {
                processor.OnStart();

                var processorType = processor.GetType();
                if (processorType.GetMethod("OnUpdate").IsOverride())
                {
                    behaviour.AddUpdateProcessor(processor);
                }

                if (processorType.GetMethod("OnLateUpdate").IsOverride())
                {
                    behaviour.AddLateUpdateProcessor(processor);
                }

                if (processorType.GetMethod("OnFixedUpdate").IsOverride())
                {
                    behaviour.AddFixedUpdateProcessor(processor);
                }

                if (processorType.GetMethod("OnDispose").IsOverride())
                {
                    behaviour.AddDisposeProcessor(processor);
                }
            }
        }
    }
}