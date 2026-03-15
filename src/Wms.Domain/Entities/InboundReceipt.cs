namespace Wms.Domain.Entities;

public sealed class InboundReceipt
{
    public InboundReceipt(
        string inboundNumber,
        string itemCode,
        string warehouseCode,
        string locationCode,
        int quantity,
        DateOnly inboundDate)
    {
        if (string.IsNullOrWhiteSpace(inboundNumber))
        {
            throw new ArgumentException("Inbound number is required.", nameof(inboundNumber));
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

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Inbound quantity must be greater than zero.");
        }

        InboundNumber = inboundNumber;
        ItemCode = itemCode;
        WarehouseCode = warehouseCode;
        LocationCode = locationCode;
        Quantity = quantity;
        InboundDate = inboundDate;
    }

    public string InboundNumber { get; }

    public string ItemCode { get; }

    public string WarehouseCode { get; }

    public string LocationCode { get; }

    public int Quantity { get; }

    public DateOnly InboundDate { get; }
}
