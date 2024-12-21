namespace Subnautica.Client.Synchronizations.Processors.Metadata
{
    using Subnautica.Network.Models.Server;
    using Subnautica.Client.Abstracts.Processors;

    public class TrashcansProcessor : MetadataProcessor
    {
        public override bool OnDataReceived(string uniqueId, TechType techType, MetadataComponentArgs packet, bool isSilence)
        {
            return true;
        }
    }
}
