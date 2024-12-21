namespace Subnautica.Events.EventArgs
{
    using System;

    public class SettingsRunInBackgroundChangingEventArgs : EventArgs
    {
        public SettingsRunInBackgroundChangingEventArgs(bool isAllowed = true)
        {
            this.IsAllowed = isAllowed;
        }

        public bool IsAllowed { get; set; }
    }
}
