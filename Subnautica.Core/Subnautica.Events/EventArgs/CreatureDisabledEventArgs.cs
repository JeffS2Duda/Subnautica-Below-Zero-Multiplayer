namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Extensions;
    using System;

    public class CreatureDisabledEventArgs : EventArgs
    {
        public CreatureDisabledEventArgs(global::Creature creature)
        {
            this.Instance = creature;
            this.UniqueId = creature.gameObject.GetIdentityId();
            this.TechType = creature.gameObject.GetTechType();
        }

        public global::Creature Instance { get; set; }

        public string UniqueId { get; set; }

        public TechType TechType { get; set; }
    }
}
