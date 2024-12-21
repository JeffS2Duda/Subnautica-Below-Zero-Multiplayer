namespace Subnautica.Events.EventArgs
{
    using System;

    using UnityEngine;

    public class SubNameInputDeselectedEventArgs : EventArgs
    {
        public SubNameInputDeselectedEventArgs(string uniqueId, TechType techType, string name, Color baseColor, Color stripeColor1, Color stripeColor2, Color nameColor)
        {
            this.UniqueId = uniqueId;
            this.TechType = techType;
            this.Name = name;
            this.BaseColor = baseColor;
            this.StripeColor1 = stripeColor1;
            this.StripeColor2 = stripeColor2;
            this.NameColor = nameColor;
        }

        public string UniqueId { get; set; }

        public string Name { get; set; }

        public Color BaseColor { get; set; }

        public Color StripeColor1 { get; set; }

        public Color StripeColor2 { get; set; }

        public Color NameColor { get; set; }

        public TechType TechType { get; set; }
    }
}
