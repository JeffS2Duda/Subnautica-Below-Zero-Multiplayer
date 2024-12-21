namespace Subnautica.Network.Models.Server
{
    using MessagePack;

    using Subnautica.API.Enums;
    using Subnautica.Network.Models.Core;

    [MessagePackObject]
    public class WeatherChangedArgs : NetworkPacket
    {
        [Key(0)]
        public override ProcessType Type { get; set; } = ProcessType.WeatherChanged;

        [Key(5)]
        public WeatherDangerLevel DangerLevel { get; set; }

        [Key(6)]
        public float StartTime { get; set; }

        [Key(7)]
        public float Duration { get; set; }

        [Key(8)]
        public float WindDir { get; set; }

        [Key(9)]
        public float WindSpeed { get; set; }

        [Key(10)]
        public float FogDensity { get; set; }

        [Key(11)]
        public float FogHeight { get; set; }

        [Key(12)]
        public float SmokinessIntensity { get; set; }

        [Key(13)]
        public float SnowIntensity { get; set; }

        [Key(14)]
        public float CloudCoverage { get; set; }

        [Key(15)]
        public float RainIntensity { get; set; }

        [Key(16)]
        public float HailIntensity { get; set; }

        [Key(17)]
        public float MeteorIntensity { get; set; }

        [Key(18)]
        public float LightningIntensity { get; set; }

        [Key(19)]
        public float Temperature { get; set; }

        [Key(20)]
        public float AuroraBorealisIntensity { get; set; }

        [Key(21)]
        public bool IsProfile { get; set; }
    }
}
