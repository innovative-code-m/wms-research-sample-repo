using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryDailyStockSnapshotRepository : IDailyStockSnapshotRepository
{
    private readonly Dictionary<DateOnly, List<DailyStockSnapshot>> snapshotsByDate = [];

    public void SaveAll(DateOnly snapshotDate, IReadOnlyList<DailyStockSnapshot> snapshots)
    {
        snapshotsByDate[snapshotDate] = snapshots.ToList();
    }

    public IReadOnlyList<DailyStockSnapshot> List(DateOnly snapshotDate)
    {
        if (!snapshotsByDate.TryGetValue(snapshotDate, out var snapshots))
        {
            return [];
        }

        return snapshots.ToList();
    }
}
