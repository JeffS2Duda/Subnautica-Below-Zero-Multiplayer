namespace Subnautica.Client.Synchronizations.Processors.Story
{
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;
    using System.Collections;
    using UnityEngine;
    using ServerModel = Subnautica.Network.Models.Server;

    public class ShieldBaseProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.StoryShieldBaseArgs>();
            if (packet.IsEntered)
            {
                UWE.CoroutineHost.StartCoroutine(this.PlayFirstEnterDirector(packet.Time - DayNightCycle.main.timePassedAsFloat));
            }

            return true;
        }

        private IEnumerator PlayFirstEnterDirector(float leftTime)
        {
            if (leftTime > 0f)
            {
                yield return new WaitForSecondsRealtime(leftTime);
            }

            var powerRoom = UnityEngine.GameObject.FindObjectOfType<PowerRoomState>();
            if (powerRoom && powerRoom.transform.parent)
            {
                var component = powerRoom.transform.parent.gameObject.GetComponentInChildren<OnTouch>();
                if (component && global::Player.main.TryGetComponent<Collider>(out var collider))
                {
                    component.onTouch.Invoke(collider);
                }
            }
        }

        public static void OnShieldBaseEnterTriggering(ShieldBaseEnterTriggeringEventArgs ev)
        {
            ev.IsAllowed = false;

            ShieldBaseProcessor.SendPacketToServer(true);
        }

        public static void SendPacketToServer(bool isEntering)
        {
            ServerModel.StoryShieldBaseArgs result = new ServerModel.StoryShieldBaseArgs()
            {
                IsEntered = true,
            };

            NetworkClient.SendPacket(result);
        }
    }
}
