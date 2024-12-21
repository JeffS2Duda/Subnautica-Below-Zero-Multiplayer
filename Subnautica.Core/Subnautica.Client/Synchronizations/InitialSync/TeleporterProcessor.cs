namespace Subnautica.Client.Synchronizations.InitialSync
{
    using Subnautica.API.Features;

    public class TeleporterProcessor
    {
        public static void OnTeleporterInitialized()
        {
            if (Network.Session.Current.ActivatedTeleporters?.Count > 0)
            {
                foreach (var teleporterId in Network.Session.Current.ActivatedTeleporters)
                {
                    TeleporterManager.ActivateTeleporter(teleporterId);
                }
            }
        }
    }
}
