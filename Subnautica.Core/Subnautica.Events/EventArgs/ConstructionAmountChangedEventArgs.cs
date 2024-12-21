namespace Subnautica.Events.EventArgs
{
    using System;
    public class ConstructionAmountChangedEventArgs : EventArgs
    {
        public ConstructionAmountChangedEventArgs(TechType techType, float constructedAmount, bool isConstruct, string uniqueId)
        {
            this.TechType    = techType;
            this.UniqueId    = uniqueId;
            this.IsConstruct = isConstruct;
            this.Amount      = constructedAmount;
        }

        public TechType TechType { get; private set; }

        public float Amount { get; private set; }

        public bool IsConstruct { get; private set; }

        public string UniqueId { get; private set; }
    }
}
