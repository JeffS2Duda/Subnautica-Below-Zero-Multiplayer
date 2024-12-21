namespace Subnautica.Events.EventArgs
{
    using System;

    public class PictureFrameImageSelectingEventArgs : EventArgs
    {
        public PictureFrameImageSelectingEventArgs(string uniqueId, string imagename, byte[] imageData, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.ImageName = imagename;
            this.ImageData = imageData;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public string ImageName { get; set; }

        public byte[] ImageData { get; set; }

        public bool IsAllowed { get; set; }
    }
}
