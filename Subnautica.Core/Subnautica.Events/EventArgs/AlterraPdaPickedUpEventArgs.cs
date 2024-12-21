namespace Subnautica.Events.EventArgs
{
    using System;

    public class AlterraPdaPickedUpEventArgs : EventArgs
    {
        public AlterraPdaPickedUpEventArgs(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public string UniqueId { get; set; }
    }
}
