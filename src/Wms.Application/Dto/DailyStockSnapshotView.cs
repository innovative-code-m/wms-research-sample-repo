namespace Wms.Application.Dto;

public sealed record DailyStockSnapshotView(
    DateOnly SnapshotDate,
    string ItemCode,
    string ItemName,
    string WarehouseCode,
    string LocationCode,
    int Quantity);
