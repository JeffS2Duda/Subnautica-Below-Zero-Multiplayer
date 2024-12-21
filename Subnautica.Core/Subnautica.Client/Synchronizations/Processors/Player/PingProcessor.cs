namespace Subnautica.Client.Synchronizations.Processors.Player
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Client.Modules;
    using Subnautica.Client.Synchronizations.InitialSync;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using System.Diagnostics;
    using static VFXParticlesPool;
    using ServerModel = Subnautica.Network.Models.Server;

    public class PingProcessor : NormalProcessor
    {
        public StopwatchItem Timing { get; set; } = new StopwatchItem(1000f);

        public Stopwatch Ping = new Stopwatch();

        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.PingArgs>();
            if (packet.ServerTime <= 0)
            {
                return false;
            }

            if (this.Ping.IsRunning)
            {
                this.Ping.Stop();

                if (this.Ping.ElapsedMilliseconds < 250)
                {
                    WorldProcessor.SetDayNightCycle(this.GetServerTime(packet.ServerTime));
                }

                PingLatency.SetPingText(this.Ping.ElapsedMilliseconds);
            }

            return true;
        }

        public override void OnStart()
        {
            this.Ping.Reset();
        }

        public override void OnFixedUpdate()
        {
            if (World.IsLoaded && this.Timing.IsFinished())
            {
                this.Timing.Restart();

                this.SendPacketToServer();
            }
            else
            {
                if (this.Ping.IsRunning)
                {
                    PingLatency.SetPingText(this.Ping.ElapsedMilliseconds);
                }
            }
        }

        public void SendPacketToServer()
        {
            if (!this.Ping.IsRunning)
            {
                this.Ping.Restart();
            }

            NetworkClient.SendPacket(new ServerModel.PingArgs());
        }

        private double GetServerTime(double serverTime)
        {
            return serverTime + 0.04 + (NetworkClient.Client.FirstPeer.Ping / 1000.0);
        }
    }
}
