using Wms.Domain.Enums;

namespace Wms.Domain.Entities;

public sealed class OutboundOrder
{
    public OutboundOrder(
        string outboundOrderNumber,
        string itemCode,
        string warehouseCode,
        string locationCode,
        int quantity,
        DateOnly scheduledShipDate,
        string customerCode,
        OutboundOrderStatus status)
    {
        if (string.IsNullOrWhiteSpace(outboundOrderNumber))
        {
            throw new ArgumentException("Outbound order number is required.", nameof(outboundOrderNumber));
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

        if (string.IsNullOrWhiteSpace(customerCode))
        {
            throw new ArgumentException("Customer code is required.", nameof(customerCode));
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Outbound quantity must be greater than zero.");
        }

        OutboundOrderNumber = outboundOrderNumber;
        ItemCode = itemCode;
        WarehouseCode = warehouseCode;
        LocationCode = locationCode;
        Quantity = quantity;
        ScheduledShipDate = scheduledShipDate;
        CustomerCode = customerCode;
        Status = status;
    }

    public string OutboundOrderNumber { get; }

    public string ItemCode { get; }

    public string WarehouseCode { get; }

    public string LocationCode { get; }

    public int Quantity { get; }

    public DateOnly ScheduledShipDate { get; }

    public string CustomerCode { get; }

    public OutboundOrderStatus Status { get; }
}
