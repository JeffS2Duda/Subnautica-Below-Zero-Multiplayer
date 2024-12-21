namespace Subnautica.Events.EventArgs
{
    using System;

    public class CellLoadingEventArgs : EventArgs
    {
        public CellLoadingEventArgs(EntityCell entityCell, Int3 batchId, Int3 cellId, int level)
        {
            this.EntityCell = entityCell;
            this.BatchId = batchId;
            this.CellId = cellId;
            this.Level = level;
        }

        public EntityCell EntityCell { get; private set; }

        public Int3 BatchId { get; private set; }

        public Int3 CellId { get; private set; }

        public int Level { get; private set; }
    }
}
