namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class StorySignalSpawningEventArgs : EventArgs
    {
        public StorySignalSpawningEventArgs(global::Story.UnlockSignalData.SignalType signalType, Vector3 targetPosition, string targetDescription, bool isAllowed = true)
        {
            this.SignalType         = signalType;
            this.TargetPosition     = targetPosition;
            this.TargetDescription  = targetDescription;
            this.IsAllowed          = isAllowed;
        }

        public global::Story.UnlockSignalData.SignalType SignalType { get; set; }

        public Vector3 TargetPosition { get; set; }

        public string TargetDescription { get; set; }

        public bool IsAllowed { get; set; }
    }
}
