namespace Wms.Application.Dto;

public sealed record RunDailyStockAggregationResult(
    DateOnly ExecutionDate,
    int SnapshotCount,
    string ExecutionLog,
    IReadOnlyList<DailyStockSnapshotView> Snapshots);
