namespace Wms.Domain.Entities;

public sealed class Stock
{
    public Stock(string itemCode, string warehouseCode, string locationCode, int quantity)
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
            throw new ArgumentOutOfRangeException(nameof(quantity), "Stock quantity cannot be negative.");
        }

        ItemCode = itemCode;
        WarehouseCode = warehouseCode;
        LocationCode = locationCode;
        Quantity = quantity;
    }

    public string ItemCode { get; }

    public string WarehouseCode { get; }

    public string LocationCode { get; }

    public int Quantity { get; }
}
