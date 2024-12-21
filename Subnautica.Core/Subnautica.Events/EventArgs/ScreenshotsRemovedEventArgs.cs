namespace Subnautica.Events.EventArgs
{
    using System;

    public class ScreenshotsRemovedEventArgs : EventArgs
    {
        public ScreenshotsRemovedEventArgs(string imageName)
        {
            this.ImageName = imageName;
        }

        public string ImageName { get; private set; }
    }
}
