namespace Subnautica.Events.EventArgs
{
    using System;

    public class LaserCutterEventArgs : EventArgs
    {
        public LaserCutterEventArgs(string uniqueId, float amount, float maxAmount, bool isAllowed = true)
        {
            this.UniqueId = uniqueId;
            this.Amount = amount;
            this.MaxAmount = maxAmount;
            this.IsAllowed = isAllowed;
        }

        public string UniqueId { get; set; }

        public float Amount { get; set; }

        public float MaxAmount { get; set; }

        public bool IsAllowed { get; set; }
    }
}
