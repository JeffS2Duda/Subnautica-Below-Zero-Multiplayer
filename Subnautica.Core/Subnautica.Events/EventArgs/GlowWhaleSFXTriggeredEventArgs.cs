namespace Subnautica.Events.EventArgs
{
    using Subnautica.API.Enums.Creatures;
    using System;

    public class GlowWhaleSFXTriggeredEventArgs : EventArgs
    {
        public GlowWhaleSFXTriggeredEventArgs(string uniqueId, GlowWhaleSFXType sfxType)
        {
            this.UniqueId = uniqueId;
            this.SFXType = sfxType;
        }

        public string UniqueId { get; set; }

        public GlowWhaleSFXType SFXType { get; set; }
    }
}
