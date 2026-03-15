namespace Wms.Domain.Entities;

public sealed class InventoryDifference
{
    public InventoryDifference(
        string inventoryCountId,
        string itemCode,
        string warehouseCode,
        string locationCode,
        int bookQuantity,
        int countedQuantity)
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

        if (bookQuantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bookQuantity), "Book quantity cannot be negative.");
        }

        if (countedQuantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(countedQuantity), "Counted quantity cannot be negative.");
        }

        InventoryCountId = inventoryCountId;
        ItemCode = itemCode;
        WarehouseCode = warehouseCode;
        LocationCode = locationCode;
        BookQuantity = bookQuantity;
        CountedQuantity = countedQuantity;
        DifferenceQuantity = countedQuantity - bookQuantity;
    }

    public string InventoryCountId { get; }

    public string ItemCode { get; }

    public string WarehouseCode { get; }

    public string LocationCode { get; }

    public int BookQuantity { get; }

    public int CountedQuantity { get; }

    public int DifferenceQuantity { get; }
}
