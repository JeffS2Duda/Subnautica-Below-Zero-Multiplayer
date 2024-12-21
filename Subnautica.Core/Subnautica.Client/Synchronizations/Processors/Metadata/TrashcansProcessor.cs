namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.Client.Abstracts.Processors;
    using Subnautica.Network.Models.Server;

    public class TrashcansProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(string uniqueId, TechType techType, MetadataComponentArgs packet, bool isSilence)
        {
            return true;
        }
    }
}
