namespace Subnautica.Server.Processors
{
    using Subnautica.Network.Models.Core;
    using Subnautica.Server.Abstracts.Processors;
    using Subnautica.Server.Core;
    using System;

    public class NoneProcessor : NormalProcessor
    {
        public override bool OnExecute(AuthorizationProfile profile, NetworkPacket networkPacket)
        {
            throw new NotImplementedException();
        }
    }
}
