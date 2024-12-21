namespace Subnautica.Client.Synchronizations.InitialSync
{
    using Subnautica.API.Enums;
    using Subnautica.API.Features;

    public class EncyclopediaProcessor
    {
        public static void OnEncylopediaInitialized()
        {
            if (Network.Session.Current.Encyclopedias != null)
            {
                using (EventBlocker.Create(ProcessType.EncyclopediaAdded))
                {
                    foreach (string encyclopedia in Network.Session.Current.Encyclopedias)
                    {
                        PDAEncyclopedia.Add(encyclopedia, false, false);
                    }
                }
            }
        }
    }
}
