namespace Subnautica.Events.EventArgs
{
    using System;

    public class SignDataChangedEventArgs : EventArgs
    {
        public SignDataChangedEventArgs(string uniqueId, TechType techType, string text, int scaleIndex, int colorIndex, bool[] elementsState, bool isBackgroundEnabled)
        {
            this.UniqueId = uniqueId;
            this.TechType = techType;
            this.Text = text;
            this.ScaleIndex = scaleIndex;
            this.ColorIndex = colorIndex;
            this.ElementsState = elementsState;
            this.IsBackgroundEnabled = isBackgroundEnabled;
        }

        public string UniqueId { get; private set; }

        public TechType TechType { get; private set; }

        public string Text { get; private set; }

        public int ScaleIndex { get; private set; }

        public int ColorIndex { get; private set; }

        public bool[] ElementsState { get; private set; }

        public bool IsBackgroundEnabled { get; private set; }
    }
}
