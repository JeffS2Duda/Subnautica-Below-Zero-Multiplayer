namespace Subnautica.Events.EventArgs
{
    using System;

    public class PictureFrameOpeningEventArgs : EventArgs
    {
        public PictureFrameOpeningEventArgs(string uniqueId, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public bool IsAllowed { get; set; }
    }
}
