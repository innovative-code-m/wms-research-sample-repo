using Wms.Domain.Entities;

namespace Wms.Application.Ports;

public interface IDailyStockSnapshotRepository
{
    void SaveAll(DateOnly snapshotDate, IReadOnlyList<DailyStockSnapshot> snapshots);

    IReadOnlyList<DailyStockSnapshot> List(DateOnly snapshotDate);
}
