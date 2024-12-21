namespace Subnautica.API.Enums
{
    using System.Collections.Generic;
    using System.Linq;

    public enum PlayerAnimationType : byte
    {
        None,
        ThermosEmpty,
        Grab,
        UsingTool,
        UsingToolAlt,
        UsingToolFirst,
        Bash,
        HoverCraftButton2,
        BeaconLandPlacement,
        HoldingThumper,
        DivereelOutofammo,
        DivereelReset,
        Jump,
        Diving,
        DivingLand,
        SpikeytrapAttached,
    }

    public static class PlayerAnimationTypeExtensions
    {
        public static List<string> Animations { get; set; } = new List<string>()
        {
            "none",
            "thermos_empty",
            "grab",
            "using_tool",
            "using_tool_alt",
            "using_tool_first",
            "bash",
            "hovercraft_button_2",
            "beacon_landplacement",
            "holding_thumper",
            "divereel_outofammo",
            "divereel_reset",
            "jump",
            "diving",
            "diving_land",
            "spikeytrap_attached",
        };

        public static string ToEnumString(this PlayerAnimationType type)
        {
            var typeId = (byte)type;

            return Animations.ElementAt(typeId);
        }

        public static PlayerAnimationType ToPlayerAnimationType(this string type)
        {
            var typeId = Animations.IndexOf(type);
            if (typeId == -1)
            {
                return PlayerAnimationType.None;
            }

            return (PlayerAnimationType)typeId;
        }
    }
}
