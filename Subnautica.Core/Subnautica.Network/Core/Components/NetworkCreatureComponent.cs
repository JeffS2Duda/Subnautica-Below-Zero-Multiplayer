namespace Subnautica.Network.Core.Components
{
    using MessagePack;
    using System;
    using CreatureModel = Subnautica.Network.Models.Creatures;

    [Union(0, typeof(CreatureModel.GlowWhale))]
    [Union(1, typeof(CreatureModel.CrashFish))]
    [Union(2, typeof(CreatureModel.LilyPaddler))]
    [MessagePackObject]
    public abstract class NetworkCreatureComponent
    {
        public T GetComponent<T>()
        {
            if (this is T)
            {
                return (T)Convert.ChangeType(this, typeof(T));
            }

            return (default(T));
        }
    }
}
