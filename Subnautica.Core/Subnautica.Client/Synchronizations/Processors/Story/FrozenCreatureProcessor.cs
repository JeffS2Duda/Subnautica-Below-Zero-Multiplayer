namespace Subnautica.Client.Synchronizations.Processors.Story
{
    using FMODUnity;

    using Subnautica.API.Enums;
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts;
    using Subnautica.Client.Core;
    using Subnautica.Events.EventArgs;
    using Subnautica.Network.Models.Core;

    using UnityEngine;

    using ServerModel = Subnautica.Network.Models.Server;

    public class FrozenCreatureProcessor : NormalProcessor
    {
        public override bool OnDataReceived(NetworkPacket networkPacket)
        {
            var packet = networkPacket.GetPacket<ServerModel.StoryFrozenCreatureArgs>();
            if (packet.CinematicType == StoryCinematicType.StoryFrozenCreatureSample)
            {
                if (ZeroPlayer.IsPlayerMine(packet.GetPacketOwnerId()))
                {
                    World.DestroyItemFromPlayer(TechType.FrozenCreatureAntidote);
                }

                Network.Session.Current.Story.FrozenCreature.AddSample();
            }
            else if (packet.CinematicType == StoryCinematicType.StoryFrozenCreatureInject)
            {
                Network.Session.Current.Story.FrozenCreature.Inject(packet.InjectTime);
            }

            FrozenCreatureProcessor.FrozenCreatureSync(false, ZeroPlayer.IsPlayerMine(packet.GetPacketOwnerId()));
            return true;
        }

        public static void OnMobileExtractorMachineInitialized()
        {
            FrozenCreatureProcessor.FrozenCreatureSync(true);
        }

        private static bool FrozenCreatureSync(bool isInitializing, bool isMine = false)
        {
            if (MobileExtractorMachine.main == null)
            {
                return false;
            }

            if (Network.Session.Current.Story.FrozenCreature.IsSampleAdded)
            {
                MobileExtractorMachine.main.sampleHandTarget.SetActive(false);
                MobileExtractorMachine.main.hasSample = true;

                if (isInitializing)
                {
                    MobileExtractorMachine.main.SetCanisterState(true);
                }
                else
                {
                    if (isMine)
                    {
                        MobileExtractorMachine.main.sampleInsertedStoryGoal.Trigger();
                    }
                }
            }

            if (Network.Session.Current.Story.FrozenCreature.IsInjected)
            {
                if (DayNightCycle.main.timePassedAsFloat >= MobileExtractorMachine.main.director.duration + Network.Session.Current.Story.FrozenCreature.InjectTime)
                {
                    MobileExtractorMachine.main.inPosition = true;
                    MobileExtractorMachine.main.director.GotoAndPlay(MobileExtractorMachine.main.labelTrack, MobileExtractorMachine.main.labelSampleExtracted);
                }
                else
                {
                    MobileExtractorMachine.main.director.MultiplayerPlay();
                    MobileExtractorMachine.main.extractFirstSound.Play();
                    MobileExtractorMachine.main.busy = true;

                    var leftTime = Mathf.Abs(DayNightCycle.main.timePassedAsFloat - Network.Session.Current.Story.FrozenCreature.InjectTime);
                    if (leftTime > 1f)
                    {
                        MobileExtractorMachine.main.director.time = MobileExtractorMachine.main.director.duration - leftTime;
                    }

                    var console = UnityEngine.GameObject.FindObjectOfType<MobileExtractorConsole>();
                    if (console)
                    {
                        if (console.soundStart)
                        {
                            RuntimeManager.PlayOneShotAttached(console.soundStart.path, console.soundOrigin);
                        }

                        console.StartCoroutine(console.PlayEndSound());
                    }
                }
            }

            return true;
        }

        public static void OnMobileExtractorMachineSampleAdding(MobileExtractorMachineSampleAddingEventArgs ev)
        {
            ev.IsAllowed = false;

            FrozenCreatureProcessor.SendPacketToServer(StoryCinematicType.StoryFrozenCreatureSample);
        }

        public static void OnMobileExtractorConsoleUsing(MobileExtractorConsoleUsingEventArgs ev)
        {
            ev.IsAllowed = false;

            FrozenCreatureProcessor.SendPacketToServer(StoryCinematicType.StoryFrozenCreatureInject);
        }

        public static void SendPacketToServer(StoryCinematicType cinematicType = StoryCinematicType.None)
        {
            ServerModel.StoryFrozenCreatureArgs result = new ServerModel.StoryFrozenCreatureArgs()
            {
                CinematicType = cinematicType
            };

            NetworkClient.SendPacket(result);
        }
    }
}
