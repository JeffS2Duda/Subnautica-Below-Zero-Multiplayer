namespace Subnautica.Events.EventArgs
{
    using System;

    public class MenuSaveUpdateLoadedButtonStateEventArgs : EventArgs
    {
        public MenuSaveUpdateLoadedButtonStateEventArgs(MainMenuLoadButton button)
        {
            Button = button;
        }

        public MainMenuLoadButton Button { get; set; }
    }
}
