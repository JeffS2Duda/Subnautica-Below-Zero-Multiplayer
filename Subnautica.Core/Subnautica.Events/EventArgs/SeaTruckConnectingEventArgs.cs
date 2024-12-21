namespace Subnautica.Events.EventArgs
{
    using System;

    public class SeaTruckConnectingEventArgs : EventArgs
    {
        public SeaTruckConnectingEventArgs(string frontModuleId, string backModuleId, string firstModuleId, bool isConnect, bool isMoonpoolExpansion, bool isAllowed = true)
        {
            this.FrontModuleId = frontModuleId;
            this.BackModuleId = backModuleId;
            this.FirstModuleId = firstModuleId;
            this.IsConnect = isConnect;
            this.IsMoonpoolExpansion = isMoonpoolExpansion;
            this.IsAllowed = isAllowed;
        }

        public string FrontModuleId { get; set; }

        public string BackModuleId { get; set; }

        public string FirstModuleId { get; set; }

        public bool IsConnect { get; set; }

        public bool IsMoonpoolExpansion { get; set; }

        public bool IsAllowed { get; set; }
    }
}
