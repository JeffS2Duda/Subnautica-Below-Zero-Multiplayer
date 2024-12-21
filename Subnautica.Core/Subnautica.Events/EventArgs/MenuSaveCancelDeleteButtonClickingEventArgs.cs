namespace Subnautica.Events.EventArgs
{
    using System;

    public class MenuSaveCancelDeleteButtonClickingEventArgs : EventArgs
    {
        public MenuSaveCancelDeleteButtonClickingEventArgs(string sessiondId, bool isRunAnimation = false, bool isAllowed = true)
        {
            SessionId = sessiondId;
            IsRunAnimation = isRunAnimation;
            IsAllowed = isAllowed;
        }

        public string SessionId { get; set; }

        public bool IsRunAnimation { get; set; }

        public bool IsAllowed { get; set; }
    }
}
