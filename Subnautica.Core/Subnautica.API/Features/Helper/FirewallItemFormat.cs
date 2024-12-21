namespace Subnautica.API.Features.Helper
{
    public class FirewallItemFormat
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Path { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsPublicProfile { get; set; }

        public bool IsPrivateProfile { get; set; }

        public bool IsDomainProfile { get; set; }

        public bool IsUdp { get; set; }

        public bool IsTcp { get; set; }

        public bool IsAllow { get; set; }
    }
}
