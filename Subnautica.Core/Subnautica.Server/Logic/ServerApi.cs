using Subnautica.API.Features;
using Subnautica.Server.Abstracts;
using Subnautica.Server.Events.EventArgs;
using System;
using UWE;

namespace Subnautica.Server.Logic
{
    public class ServerApi : BaseLogic
    {
        private StopwatchItem Timing { get; set; } = new StopwatchItem(10000f, null, true);

        private byte ErrorCount { get; set; } = 0;
        private bool IsCompleted { get; set; } = false;

        public override void OnUnscaledFixedUpdate(float fixedDeltaTime)
        {
            bool flag = this.Timing.IsFinished();
            if (flag)
            {
                this.Timing.Restart();
                CoroutineHost.StartCoroutine(API.Features.Network.InviteCode.PingToServerAsync(new Action<bool>(this.OnPingCallback)));
            }
        }

        public void OnPlayerDisconnected(PlayerDisconnectedEventArgs ev)
        {
            bool flag = !ev.Player.IsHost;
            if (flag)
            {
                CoroutineHost.StartCoroutine(API.Features.Network.InviteCode.LeaveFromServerAsync(ev.Player.IpAddress, null));
            }
        }

        private void OnPingCallback(bool isError)
        {
            if (isError)
            {
                this.ErrorCount++;
            }
            else
            {
                this.ErrorCount = 0;
            }
            bool flag = this.ErrorCount >= 3 && !this.IsCompleted;
            if (flag)
            {
                this.IsCompleted = true;
                Log.Info("IsCompletedTrue");
                Core.Server.Instance.Dispose(false);
                ZeroGame.QuitToMainMenu();
            }
        }
    }
}
