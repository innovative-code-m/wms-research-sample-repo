namespace Wms.Domain.Entities;

public sealed class DailyStockSnapshot
{
    public DailyStockSnapshot(
        DateOnly snapshotDate,
        string itemCode,
        string warehouseCode,
        string locationCode,
        int quantity)
    {
        if (string.IsNullOrWhiteSpace(itemCode))
        {
            throw new ArgumentException("Item code is required.", nameof(itemCode));
        }

        if (string.IsNullOrWhiteSpace(warehouseCode))
        {
            throw new ArgumentException("Warehouse code is required.", nameof(warehouseCode));
        }

        if (string.IsNullOrWhiteSpace(locationCode))
        {
            throw new ArgumentException("Location code is required.", nameof(locationCode));
        }

        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Snapshot quantity cannot be negative.");
        }

        SnapshotDate = snapshotDate;
        ItemCode = itemCode;
        WarehouseCode = warehouseCode;
        LocationCode = locationCode;
        Quantity = quantity;
    }

    public DateOnly SnapshotDate { get; }

    public string ItemCode { get; }

    public string WarehouseCode { get; }

    public string LocationCode { get; }

    public int Quantity { get; }
}
