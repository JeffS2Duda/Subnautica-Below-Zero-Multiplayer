namespace Subnautica.Client.Synchronizations.InitialSync
{
    using Subnautica.API.Enums;
    using Subnautica.API.Features;

    public class ItemPinManagerProcessor
    {
        public static void OnItemPinInitialized()
        {
            if (Network.Session.Current.PlayerItemPins != null)
            {
                using (EventBlocker.Create(ProcessType.ItemPin))
                {
                    foreach (var techType in Network.Session.Current.PlayerItemPins)
                    {
                        PinManager.SetPin(techType, true);
                    }
                }
            }
        }
    }
}
