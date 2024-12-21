namespace Subnautica.API.Features
{
    using Subnautica.API.Enums;

    public abstract class SubnauticaPlugin
    {
        public virtual string Name { get; }

        public virtual SubnauticaPluginPriority Priority { get; set; } = SubnauticaPluginPriority.Medium;

        public SubnauticaPlugin()
        {
        }

        public virtual void OnEnabled()
        {
        }

        public virtual void OnDisabled()
        {
        }
    }
}
