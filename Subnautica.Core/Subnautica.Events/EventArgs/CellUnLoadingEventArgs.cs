namespace Subnautica.Events.EventArgs
{
    using System;

    public class CellUnLoadingEventArgs : EventArgs
    {
        public CellUnLoadingEventArgs(EntityCell entityCell, Int3 batchId, Int3 cellId)
        {
            this.EntityCell = entityCell;
            this.BatchId = batchId;
            this.CellId = cellId;
        }

        public EntityCell EntityCell { get; private set; }

        public Int3 BatchId { get; private set; }

        public Int3 CellId { get; private set; }
    }
}
