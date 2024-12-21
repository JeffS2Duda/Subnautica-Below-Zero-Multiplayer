namespace Subnautica.Events.EventArgs
{
    using System;

    public class SettingsPdaGamePauseChangingEventArgs : EventArgs
    {
        public SettingsPdaGamePauseChangingEventArgs(bool isAllowed = true)
        {
            IsAllowed = isAllowed;
        }

        public bool IsAllowed { get; set; }
    }
}
