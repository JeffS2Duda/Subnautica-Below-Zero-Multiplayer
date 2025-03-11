namespace Subnautica.Events.EventArgs;

using System;

public class PlanterStorageResetingEventArgs : EventArgs
{
    public PlanterStorageResetingEventArgs(string uniqueId, TechType techType, bool isLeft, bool isAllowed = true)
    {
        this.UniqueId = uniqueId;
        this.TechType = techType;
        this.IsLeft = isLeft;
        this.IsAllowed = isAllowed;
    }

    public string UniqueId { get; set; }

    public TechType TechType { get; set; }

    public bool IsLeft { get; set; }

    public bool IsAllowed { get; set; }
}