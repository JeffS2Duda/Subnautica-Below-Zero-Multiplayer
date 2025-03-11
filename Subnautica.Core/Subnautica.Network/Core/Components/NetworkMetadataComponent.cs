namespace Subnautica.Network.Core.Components
{
    using MessagePack;
    using Subnautica.Network.Models.Metadata;
    using System;

    [Union(0, typeof(Aquarium))]
    [Union(1, typeof(AromatherapyLamp))]
    [Union(2, typeof(EmmanuelPendulum))]
    [Union(3, typeof(Shower))]
    [Union(4, typeof(Sink))]
    [Union(5, typeof(SmallStove))]
    [Union(6, typeof(Toilet))]
    [Union(7, typeof(Snowman))]
    [Union(8, typeof(Crafter))]
    [Union(9, typeof(Jukebox))]
    [Union(10, typeof(JukeboxUsed))]
    [Union(11, typeof(Sign))]
    [Union(12, typeof(PictureFrame))]
    [Union(13, typeof(Trashcans))]
    [Union(14, typeof(LabTrashcan))]
    [Union(15, typeof(BioReactor))]
    [Union(16, typeof(NuclearReactor))]
    [Union(17, typeof(CoffeeVendingMachine))]
    [Union(18, typeof(Charger))]
    [Union(19, typeof(StorageItem))]
    [Union(20, typeof(StorageContainer))]
    [Union(21, typeof(Hoverpad))]
    [Union(22, typeof(Recyclotron))]
    [Union(23, typeof(Fridge))]
    [Union(24, typeof(FiltrationMachine))]
    [Union(25, typeof(Planter))]
    [Union(26, typeof(Bed))]
    [Union(27, typeof(Bench))]
    [Union(28, typeof(BulkheadDoor))]
    [Union(29, typeof(BaseControlRoom))]
    [Union(30, typeof(SpotLight))]
    [Union(31, typeof(TechLight))]
    [Union(32, typeof(StorageLocker))]
    [Union(33, typeof(BaseMoonpool))]
    [Union(34, typeof(BaseMapRoom))]
    [Union(35, typeof(BaseMoonpoolExpansionManager))]
    [Union(36, typeof(BaseWaterPark))]
    [MessagePackObject]
    public abstract class MetadataComponent
    {
        public T GetComponent<T>()
        {
            if (this is T)
            {
                return (T)Convert.ChangeType(this, typeof(T));
            }

            return default(T);
        }
    }
}
