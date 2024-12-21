namespace Subnautica.Client.Synchronizations.InitialSync
{
    using Subnautica.API.Features;
    using System.Collections;
    using UnityEngine;

    public class LifepodProcessor
    {
        public static IEnumerator OnLifePodInitialized()
        {
            if (Network.Session.Current.SupplyDrops?.Count > 0)
            {
                foreach (var supplyDrop in Network.Session.Current.SupplyDrops)
                {
                    if (supplyDrop.ZoneId != -1)
                    {
                        Processors.General.LifepodProcessor.ForceSupplyDrop(supplyDrop.Key);

                        Log.Info("LifePod UniqueId: " + supplyDrop.UniqueId + ", ZoneId: " + supplyDrop.ZoneId);

                        if (supplyDrop.StorageContainer != null && supplyDrop.StorageContainer.Items.Count > 0)
                        {
                            yield return new WaitForSecondsRealtime(0.1f);

                            Processors.General.LifepodProcessor.InitializeStorage(supplyDrop);
                        }
                    }
                }
            }
        }
    }
}
