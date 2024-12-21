namespace Subnautica.API.Features
{
    using System.Collections.Generic;

    public class TechGroup
    {
        public static List<TechType> Beds { get; set; } = new List<TechType>()
        {
            TechType.Bed1,
            TechType.Bed2,
            TechType.NarrowBed,
            TechType.BedJeremiah,
            TechType.BedSam,
            TechType.BedZeta,
            TechType.BedDanielle,
            TechType.BedEmmanuel,
            TechType.BedFred,
            TechType.BedParvan,
        };

        public static List<TechType> Chairs { get; set; } = new List<TechType>()
        {
            TechType.Bench,
            TechType.StarshipChair,
            TechType.StarshipChair2,
            TechType.StarshipChair3,
        };

        public static List<TechType> Planters { get; set; } = new List<TechType>()
        {
            TechType.PlanterPot,
            TechType.PlanterPot2,
            TechType.PlanterPot3,
            TechType.PlanterBox,
            TechType.PlanterShelf,
            TechType.FarmingTray,
        };

        public static List<TechType> EnergyConstructions { get; set; } = new List<TechType>()
        {
            TechType.SolarPanel,
            TechType.BaseNuclearReactor,
            TechType.BaseBioReactor,
            TechType.ThermalPlant,
        };

        public static List<TechType> BatteryChargers { get; set; } = new List<TechType>()
        {
            TechType.BatteryCharger,
            TechType.PowerCellCharger
        };

        public static List<TechType> Lockers { get; set; } = new List<TechType>()
        {
            TechType.Locker,
            TechType.SmallLocker,
        };

        public static List<TechType> Reactors { get; set; } = new List<TechType>()
        {
            TechType.BaseBioReactor,
            TechType.BaseNuclearReactor,
        };

        public static List<TechType> GlobalEntityTypes { get; set; } = new List<TechType>()
        {
            TechType.SpyPenguin,
            TechType.Hoverbike,
            TechType.Exosuit,
            TechType.SeaTruck,
            TechType.SeaTruckFabricatorModule,
            TechType.SeaTruckStorageModule,
            TechType.SeaTruckAquariumModule,
            TechType.SeaTruckDockingModule,
            TechType.SeaTruckSleeperModule,
            TechType.SeaTruckTeleportationModule,
            TechType.MapRoomCamera,

            TechType.Thumper,
            TechType.Constructor,
            TechType.Beacon,
        };

        public static List<string> BatteryChargerSlots = new List<string>()
        {
            "BatteryCharger1",
            "BatteryCharger2",
            "BatteryCharger3",
            "BatteryCharger4"
        };

        public static List<string> PowerCellChargerSlots = new List<string>()
        {
            "PowerCellCharger1",
            "PowerCellCharger2"
        };

        public static bool IsGlobalEntity(TechType techType)
        {
            return GlobalEntityTypes.Contains(techType);
        }

        public static byte GetBatterySlotAmount(TechType techType)
        {
            return techType == TechType.BatteryCharger ? (byte)4 : (byte)2;
        }

        public static string GetBatterySlotId(TechType techType, int index)
        {
            if (techType == TechType.BatteryCharger)
            {
                return string.Format("BatteryCharger{0}", index);
            }

            return string.Format("PowerCellCharger{0}", index);
        }

        public static string GetBaseControlRoomCustomizerId(string uniqueId)
        {
            return string.Format("{0}_Customizer", uniqueId);
        }

        public static string GetBaseControlRoomNavigateId(string uniqueId)
        {
            return string.Format("{0}_NavigateMiniMap", uniqueId);
        }
    }
}
