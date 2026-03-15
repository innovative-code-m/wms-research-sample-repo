namespace Wms.Domain.Entities;

public sealed class InventoryCount
{
    public InventoryCount(
        string inventoryCountId,
        string itemCode,
        string warehouseCode,
        string locationCode,
        int countedQuantity,
        DateOnly countedDate)
    {
        if (string.IsNullOrWhiteSpace(inventoryCountId))
        {
            throw new ArgumentException("Inventory count ID is required.", nameof(inventoryCountId));
        }

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

        if (countedQuantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(countedQuantity), "Counted quantity cannot be negative.");
        }

        InventoryCountId = inventoryCountId;
        ItemCode = itemCode;
        WarehouseCode = warehouseCode;
        LocationCode = locationCode;
        CountedQuantity = countedQuantity;
        CountedDate = countedDate;
    }

    public string InventoryCountId { get; }

    public string ItemCode { get; }

    public string WarehouseCode { get; }

    public string LocationCode { get; }

    public int CountedQuantity { get; }

    public DateOnly CountedDate { get; }
}
