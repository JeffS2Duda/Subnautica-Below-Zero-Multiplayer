namespace Subnautica.Events.Patches.Events.Creatures
{
    using HarmonyLib;

    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.API.Features.Creatures.Datas;
    using Subnautica.Events.EventArgs;

    using System;
    using System.Collections.Generic;

    using UnityEngine;

    [HarmonyPatch]
    public class AnimationChanged
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::Creature), nameof(global::Creature.OnEnable))]
        public static void OnEnable(global::Creature __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                __instance.gameObject.EnsureComponent<AnimationTrackerBehaviour>().enabled = true;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(global::Creature), nameof(global::Creature.OnDisable))]
        public static void OnDisable(global::Creature __instance)
        {
            if (Network.IsMultiplayerActive)
            {
                __instance.gameObject.EnsureComponent<AnimationTrackerBehaviour>().enabled = false;
            }
        }
    }

    public class AnimationTrackerBehaviour : MonoBehaviour
    {
        private global::Creature Creature { get; set; }

        private ushort CreatureId { get; set; }

        private BaseCreatureData Data { get; set; }

        private Dictionary<byte, byte> OldValues = new Dictionary<byte, byte>();

        private byte Result;

        private bool IsChanged;

        public void OnEnable()
        {
            this.Creature = this.GetComponent<global::Creature>();
            this.Data = CraftData.GetTechType(this.gameObject).GetCreatureData();

            if (this.Data?.HasAnimationTrackers() == true)
            {
                var uniqueId = this.gameObject.GetIdentityId();
                if (uniqueId.IsMultiplayerCreature())
                {
                    this.CreatureId = uniqueId.ToCreatureId();

                    foreach (var item in this.Data.GetAnimationTrackers())
                    {
                        this.OldValues[item.Key] = 0;
                    }
                }
                else
                {
                    this.enabled = false;
                }
            }
            else
            {
                this.enabled = false;
            }
        }

        public void FixedUpdate()
        {
            foreach (var item in this.Data.GetAnimationTrackers())
            {
                if (this.OldValues.TryGetValue(item.Key, out var oldValue))
                {
                    try
                    {
                        this.IsChanged = item.Value.OnTrackerChecking(this.Creature, oldValue, out this.Result);
                        if (this.IsChanged)
                        {
                            if (item.Value.AllowedCustomResults.Count <= 0 || item.Value.AllowedCustomResults.Contains(this.Result))
                            {
                                CreatureAnimationChangedEventArgs args = new CreatureAnimationChangedEventArgs(this.CreatureId, item.Key, this.Result);

                                Handlers.Creatures.OnAnimationChanged(args);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        this.IsChanged = false;
                        Log.Error($"AnimationTrackerBehaviour.Prefix: {e}\n{e.StackTrace}");
                    }
                    finally
                    {
                        if (this.IsChanged)
                        {
                            this.OldValues[item.Key] = this.Result;
                        }
                    }
                }
            }
        }
    }
}
