namespace Subnautica.Client.Synchronizations.Processors.Story
{
    using global::Story;

    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using ServerModel = Subnautica.Network.Models.Server;

    public class CallProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.StoryCallArgs>();
            if (packet.IsAnswered)
            {
                this.Answer(packet.GoalKey);
            }
            else
            {
                this.Decline(packet.GoalKey);
            }

            return true;
        }

        private void Answer(string data)
        {
            if (PDACalls.TryGet(data, out var callData))
            {
                StoryGoalManager.main.OnGoalComplete(callData.dialogue);
                PDALog.Add(callData.dialogue, true);

                uGUI_PopupNotification.main.current.id = null;
                uGUI_PopupNotification.main.current.data = null;
                uGUI_PopupNotification.main.Hide();
            }

            uGUI_PopupNotification.main.PlaySound(uGUI_PopupNotification.main.soundAnswer);
        }

        private void Decline(string data)
        {
            uGUI_PopupNotification.main.current = new uGUI_PopupNotification.Entry();
            uGUI_PopupNotification.main.Hide();
            uGUI_PopupNotification.main.Voicemail(data);
        }

        public static void OnStoryCalling(StoryCallingEventArgs ev)
        {
            ev.IsAllowed = false;

            ServerModel.StoryCallArgs result = new ServerModel.StoryCallArgs()
            {
                GoalKey = ev.CallGoalKey,
                TargetGoalKey = ev.TargetGoalKey,
                IsAnswered = ev.IsAnswered,
            };

            NetworkClient.SendPacket(result);
        }
    }
}
