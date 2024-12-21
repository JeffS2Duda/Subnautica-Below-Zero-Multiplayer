namespace Subnautica.Client.Synchronizations.Processors.World
{
    using Subnautica.API.Features;
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Events.EventArgs;

    public class EntitySpawnProcessor
    {
        public static void OnEntitySpawning(EntitySpawningEventArgs ev)
        {

            if (Network.StaticEntity.IsRestricted(ev.UniqueId))
            {
                ev.IsAllowed = false;
            }
        }

        public static void OnEntitySpawned(EntitySpawnedEventArgs ev)
        {
            if (ev.TechType != TechType.None && !Network.StaticEntity.IsRestricted(ev.UniqueId))
            {

                var entity = Network.StaticEntity.GetEntity(ev.UniqueId);
                if (entity != null)
                {
                    WorldEntityProcessor.ExecuteProcessor(entity, 0, true);
                }
            }
        }
    }
}
