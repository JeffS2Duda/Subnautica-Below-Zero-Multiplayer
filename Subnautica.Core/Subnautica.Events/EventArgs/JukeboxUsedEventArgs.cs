namespace Subnautica.Events.EventArgs
{
    using System;

    using Subnautica.API.Features;

    public class JukeboxUsedEventArgs : EventArgs
    {
        public JukeboxUsedEventArgs(string uniqueId, CustomProperty data, bool isSeaTruckModule)
        {
            this.UniqueId         = uniqueId;
            this.Data             = data;
            this.IsSeaTruckModule = isSeaTruckModule;
        }

        public string UniqueId { get; private set; }

        public CustomProperty Data { get; private set; }

        public bool IsSeaTruckModule { get; private set; }
    }
}
