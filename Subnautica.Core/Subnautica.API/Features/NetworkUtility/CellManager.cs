namespace Subnautica.API.Features.NetworkUtility
{
    using System.Collections.Generic;

    using Subnautica.API.Extensions;
    using Subnautica.Network.Structures;

    public class CellManager
    {
        public readonly Dictionary<Int3, WorldStreamerBatchItem> Batches = new Dictionary<Int3, WorldStreamerBatchItem>(Int3.equalityComparer);

        public void SetLoaded(Int3 batchId, Int3 cellId, bool isLoaded)
        {
            if (Batches.TryGetValue(batchId, out var batch))
            {
                batch.Cells[cellId] = isLoaded;
            }
            else
            {
                Batches[batchId] = new WorldStreamerBatchItem();
                Batches[batchId].Cells[cellId] = isLoaded;
            }
        }

        public bool IsLoaded(Int3 batchId, Int3 cellId)
        {
            if (Batches.TryGetValue(batchId, out var batch))
            {
                return batch.IsLoaded(cellId);
            }

            return false;
        }

        public bool IsLoaded(ZeroVector3 position)
        {
            var block     = LargeWorldStreamer.main.GetBlock(position.ToVector3());
            var batchId   = block / LargeWorldStreamer.main.blocksPerBatch;
            var int3      = block % LargeWorldStreamer.main.blocksPerBatch;
            var cellLevel = 0;

            var cellSize = BatchCells.GetCellSize(cellLevel, LargeWorldStreamer.main.blocksPerBatch);
            var cellId   = int3 / cellSize;

            return IsLoaded(batchId, cellId);
        }

        public void Dispose()
        {
            this.Batches.Clear();
        }
    }

    public class WorldStreamerBatchItem
    {
        public readonly Dictionary<Int3, bool> Cells = new Dictionary<Int3, bool>(Int3.equalityComparer);

        public bool IsLoaded(Int3 cellId)
        {
            return this.Cells.TryGetValue(cellId, out var isLoaded) && isLoaded;
        }
    }
}
