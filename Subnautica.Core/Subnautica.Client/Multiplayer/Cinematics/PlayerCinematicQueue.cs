namespace Subnautica.Client.Multiplayer.Cinematics
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Helper;
    using Subnautica.Client.Extensions;
    using Subnautica.Client.MonoBehaviours.Player;

    using UWE;

    public class PlayerCinematicQueue
    {
        private static Dictionary<string, Queue<PlayerCinematicQueueItem>> Queues = new Dictionary<string, Queue<PlayerCinematicQueueItem>>();

        private static List<string> QueueStatus = new List<string>();

        public static void Initialize()
        {
            World.OnGameObjectDestroyingAction = PlayerCinematicQueue.GameObjectDestroyingAction;
        }

        public static void AddQueue(CinematicController cinematicController, Action cinematicAction, string playerId, string uniqueId, GenericProperty property = null)
        {
            if (!Queues.ContainsKey(playerId))
            {
                Queues[playerId] = new Queue<PlayerCinematicQueueItem>();
            }

            Queues[playerId].Enqueue(new PlayerCinematicQueueItem(cinematicController, cinematicAction, uniqueId, property));
            ConsumeQueue(playerId);
        }

        public static bool IsQueueRunning(string playerId)
        {
            return QueueStatus.Contains(playerId);
        }

        private static void ConsumeQueue(string playerId)
        {
            if (!IsQueueRunning(playerId))
            {
                CoroutineHost.StartCoroutine(ConsumeQueueAsync(playerId));
            }
        }

        private static IEnumerator ConsumeQueueAsync(string playerId)
        {
            if (Queues.TryGetValue(playerId, out var queues))
            {
                QueueStatus.Add(playerId);

                while (queues.Count > 0)
                {
                    var item = queues.Dequeue();
                    if (item.CinematicController)
                    {
                        while (IsUsingTarget(item.UniqueId, playerId))
                        {
                            Log.Info("BUSY. REPORT! I AM WAITING... => " + playerId + ", IT: " + item.UniqueId);
                            yield return CoroutineUtils.waitForFixedUpdate;
                        }

                        if (item.CinematicController.ZeroPlayer.IsDestroyed)
                        {
                            continue;
                        }

                        item.CinematicController.ZeroPlayer.ResetCinematics();
                        item.CinematicController.SetValid(true);
                        item.CinematicController.OnStart(item);

                        if (item.CinematicController.IsValid())
                        {
                            item.CinematicController.ZeroPlayer.EnableCinematicMode();
                            item.CinematicController.ZeroPlayer.Animator.Rebind();

                            item.CinematicController.SetUniqueId(item.UniqueId);
                            item.CinematicController.OnResetAnimations(item);

                            yield return item.CinematicController.OnResetAnimationsAsync(item);

                            if (item.CinematicController && item.CinematicController.IsValid())
                            {
                                item.CinematicController.SetProperties(item.Properties);

                                try
                                {
                                    item.CinematicAction?.Invoke();
                                }
                                catch (Exception e)
                                {
                                    Log.Error($"PlayerCinematicQueue.ConsumeQueueAsync Exception: {e}");
                                }
                            }

                            if (item.CinematicController && item.CinematicController.IsNotActiveResetCinematic && !item.CinematicController.IsCinematicModeActive && item.CinematicController.ZeroPlayer != null)
                            {
                                item.CinematicController.ZeroPlayer.DisableCinematicMode();
                                item.CinematicController.ZeroPlayer.Animator.Rebind();
                            }
                        }
                    }
                }

                QueueStatus.Remove(playerId);
            }
        }

        private static bool IsUsingTarget(string uniqueId, string playerId)
        {
            if (Interact.IsBlocked(uniqueId, playerId, true))
            {
                return true;
            }

            foreach (var player in ZeroPlayer.GetPlayers())
            {
                if (playerId != player.UniqueId && player.GetCinematics().Any(q => q.UniqueId == uniqueId && q.IsCinematicModeActive))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool GameObjectDestroyingAction(string uniqueId)
        {
            try
            {
                ZeroPlayer.CurrentPlayer.ResetCinematicsByUniqueId(uniqueId);
            }
            catch (Exception ex)
            {
                Log.Error($"PlayerCinematicQueue.GameObjectDestroyingAction Exception: {ex}");
            }

            return true;
        }

        public static void Dispose()
        {
            PlayerCinematicQueue.Queues.Clear();
            PlayerCinematicQueue.QueueStatus.Clear();
        }
    }

    public class PlayerCinematicQueueItem
    {   
        public string UniqueId { get; private set; }

        public Action CinematicAction { get; private set; }

        public CinematicController CinematicController { get; private set; }

        public List<GenericProperty> Properties = new List<GenericProperty>();

        public PlayerCinematicQueueItem(CinematicController cinematicController, Action cinematicAction, string uniqueId, GenericProperty property = null)
        {
            this.CinematicController = cinematicController;
            this.CinematicAction     = cinematicAction;
            this.UniqueId            = uniqueId;

            if (property != null)
            {
                this.Properties.Add(property);
            }
        }

        public void RegisterProperty(string key, object value)
        {
            this.Properties.Add(new GenericProperty(key, value));
        }

        public void ClearProperties()
        {
            this.Properties.Clear();
        }
    }
}
