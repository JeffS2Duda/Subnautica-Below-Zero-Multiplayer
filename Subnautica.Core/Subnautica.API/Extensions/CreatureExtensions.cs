namespace Subnautica.API.Extensions
{
    using Subnautica.API.Features;
    using Subnautica.API.Features.Creatures;
    using Subnautica.API.Features.Creatures.Datas;
    using Subnautica.Network.Models.Creatures;
    using System;
    using System.Collections;
    using UnityEngine;

    public static class CreatureExtensions
    {
        public static bool IsMultiplayerCreature(this string creatureId)
        {
            return creatureId.Contains("MultiplayerCreature_");
        }

        public static string ToCreatureStringId(this ushort creatureId)
        {
            return string.Format("MultiplayerCreature_" + creatureId);
        }

        public static ushort ToCreatureId(this string uniqueId)
        {
            return Convert.ToUInt16(uniqueId.Replace("MultiplayerCreature_", ""));
        }

        public static bool IsCanBeAttacked(this TechType techType)
        {
            var creatureData = CreatureData.Instance.GetCreatureData(techType);
            return creatureData == null ? false : creatureData.IsCanBeAttacked;
        }
        public static BaseCreatureData GetCreatureData(this TechType techType)
        {
            return CreatureData.Instance.GetCreatureData(techType);
        }

        public static bool IsSynchronizedCreature(this TechType techType)
        {
            return CreatureData.Instance.IsExists(techType);
        }

        public static bool IsSynchronized(this global::Creature creature)
        {
            return CraftData.GetTechType(creature.gameObject).IsSynchronizedCreature();
        }

        public static void StopPrevAction(this global::Creature creature)
        {
            if (creature.prevBestAction)
            {
                creature.prevBestAction.StopPerform(Time.time);
                creature.prevBestAction = null;
            }
        }

        public static MultiplayerCreature GetCreatureObject(this MultiplayerCreatureItem creature)
        {
            if (Network.Creatures.TryGetActiveCreatureObject(creature.Id, out var creatureObject))
            {
                return creatureObject;
            }

            return null;
        }

        public static void Spawn(this MultiplayerCreatureItem creature)
        {
            var creatureObject = creature.GetCreatureObject();
            if (creatureObject != null)
            {
                creatureObject.SetCreatureItem(creature);
                creatureObject.Spawn();
            }
        }

        public static void Disable(this MultiplayerCreatureItem creature)
        {
            var creatureObject = creature.GetCreatureObject();
            if (creatureObject != null)
            {
                creatureObject.Disable();
            }

            creature.RemoveCreatureObject();
        }

        public static void ChangeOwnership(this MultiplayerCreatureItem creature)
        {
            var creatureObject = creature.GetCreatureObject();
            if (creatureObject != null)
            {
                creatureObject.ChangeOwnership();
            }
        }

        public static void SetCreatureObject(this MultiplayerCreatureItem creature, MultiplayerCreature creatureObject)
        {
            API.Features.Network.Creatures.SetActiveCreatureObject(creature.Id, creatureObject);
        }

        public static void RemoveCreatureObject(this MultiplayerCreatureItem creature)
        {
            API.Features.Network.Creatures.RemoveActiveCreatureObject(creature.Id);
        }

        public static IEnumerator BornAsync(this GameObject creatureGameObject, TaskResult<GameObject> taskResult)
        {
            taskResult.Set(null);

            if (creatureGameObject && creatureGameObject.TryGetComponent<global::CreatureEgg>(out var creatureEgg) && creatureEgg.TryGetComponent<global::WaterParkItem>(out var waterParkItem))
            {
                waterParkItem.SetWaterPark(null);

                if (KnownTech.Add(creatureEgg.eggType, false))
                {
                    ErrorMessage.AddMessage(Language.main.GetFormat<string>("EggDiscovered", Language.main.Get(creatureEgg.eggType.AsString())));
                }

                if (creatureEgg.creaturePrefab != null)
                {
                    var result = AddressablesUtility.InstantiateAsync(creatureEgg.creaturePrefab.RuntimeKey.ToString(), position: Vector3.zero, rotation: Quaternion.identity, awake: false);

                    yield return result;

                    var gameObject = result.GetResult();
                    if (gameObject.TryGetComponent<global::WaterParkCreature>(out var waterParkCreature))
                    {
                        waterParkCreature.age = 0.0f;
                        waterParkCreature.bornInside = true;
                        waterParkCreature.SetMatureTime();
                        waterParkCreature.InitializeCreatureBornInWaterPark();
                    }

                    gameObject.EnsureComponent<global::Pickupable>();

                    taskResult.Set(gameObject);
                }

                creatureEgg.liveMixin.Kill();
                creatureEgg.gameObject.Destroy();
            }
        }
    }
}