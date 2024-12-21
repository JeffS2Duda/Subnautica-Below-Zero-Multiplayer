namespace Subnautica.Client.Extensions
{
    using Subnautica.API.Features;
    using Subnautica.Client.Multiplayer.Cinematics;
    using Subnautica.Client.Multiplayer.Cinematics.CreatureCinematics;
    using UnityEngine;

    public static class PlayerCinematicCreatureExtensions
    {
        public static bool OnHandClickGlowWhaleRideStart(this ZeroPlayer player, string creatureId)
        {
            player.ResetCinematicsByUniqueId(creatureId);

            var glowWhaleRide = Network.Identifier.GetComponentByGameObject<global::GlowWhaleRide>(creatureId);
            if (glowWhaleRide == null)
            {
                return false;
            }

            Inventory.main.ReturnHeld();

            var mainPlayer = global::Player.main;
            mainPlayer.EnterSittingMode();
            mainPlayer.playerController.SetEnabled(enabled: false);
            mainPlayer.GetPDA().SetIgnorePDAInput(ignore: true);
            mainPlayer.ridingCreature = true;
            mainPlayer.playerAnimator.SetBool(glowWhaleRide.playerRideAnimation, true);
            mainPlayer.playerAnimator.SetBool(GlowWhaleRide.animCinematicWithPitch, true);

            MainCameraControl.main.cinematicMode = true;
            MainCameraControl.main.lookAroundMode = true;
            MainCameraControl.main.viewModel.localRotation = Quaternion.identity;

            glowWhaleRide.animator.SetBool(glowWhaleRide.rideAnimation, value: true);
            glowWhaleRide.Subscribe(mainPlayer, true);

            glowWhaleRide.director.SetBinding("Player", mainPlayer.playerAnimator, typeof(Animator));
            glowWhaleRide.director.Play();
            glowWhaleRide.attachSfx.Play();

            SNCameraRoot.main.SetNearClip(glowWhaleRide.nearClip);

            glowWhaleRide.ridden = true;
            glowWhaleRide.timeRideStart = Time.time;

            BehaviourUpdateUtils.RegisterForUpdate(glowWhaleRide);
            BehaviourUpdateUtils.RegisterForLateUpdate(glowWhaleRide);
            return true;
        }

        public static bool OnHandClickGlowWhaleEyeCinematicStart(this ZeroPlayer player, string uniqueId)
        {
            player.ResetCinematicsByUniqueId(uniqueId);

            var climb = Network.Identifier.GetComponentByGameObject<global::PlayerCinematicController>(uniqueId);
            if (climb == null)
            {
                return false;
            }

            Inventory.main.ReturnHeld();

            climb.StartCinematicMode(global::Player.main);
            return true;
        }

        public static void StartGlowWhaleRideCinematic(this ZeroPlayer player, string uniqueId)
        {
            var cinematic = player.GetCinematic<GlowWhaleCinematic>();
            if (cinematic)
            {
                PlayerCinematicQueue.AddQueue(cinematic, cinematic.StartRideCinematic, player.UniqueId, uniqueId);
            }
        }

        public static void StopGlowWhaleRideCinematic(this ZeroPlayer player, string uniqueId)
        {
            var cinematic = player.GetCinematic<GlowWhaleCinematic>();
            if (cinematic)
            {
                PlayerCinematicQueue.AddQueue(cinematic, cinematic.StopRideCinematic, player.UniqueId, uniqueId);
            }
        }

        public static void StartGlowWhaleEyeCinematic(this ZeroPlayer player, string uniqueId)
        {
            var cinematic = player.GetCinematic<GlowWhaleCinematic>();
            if (cinematic)
            {
                PlayerCinematicQueue.AddQueue(cinematic, cinematic.StartEyeInteractCinematic, player.UniqueId, uniqueId);
            }
        }

        public static void StartLeviathanMeleeAttackCinematic(this ZeroPlayer player, string uniqueId)
        {
            var cinematic = player.GetCinematic<LeviathanMeleeAttackCinematic>();
            if (cinematic)
            {
                PlayerCinematicQueue.AddQueue(cinematic, cinematic.StartMeleeAttack, player.UniqueId, uniqueId);
            }
        }
    }
}
