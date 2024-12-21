namespace Subnautica.Events.EventArgs
{
    using System;

    public class QuittingToMainMenuEventArgs : EventArgs
    {
        public QuittingToMainMenuEventArgs(bool isQuitToDesktop, bool isAllowed = true)
        {
            this.IsQuitToDesktop = isQuitToDesktop;
            this.IsAllowed       = isAllowed;
        }

        public bool IsQuitToDesktop { get; private set; }

        public bool IsAllowed { get; set; }
    }
}
