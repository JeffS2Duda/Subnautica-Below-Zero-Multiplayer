namespace Subnautica.Events.EventArgs
{
    using System;

    public class MenuSaveLoadButtonClickingEventArgs : EventArgs
    {
        public MenuSaveLoadButtonClickingEventArgs(string sessiondId, bool isAllowed = true)
        {
            SessionId = sessiondId;
            IsAllowed = isAllowed;
        }

        public string SessionId { get; set; }

        public bool IsAllowed { get; set; }
    }
}
