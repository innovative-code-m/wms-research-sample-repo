namespace Wms.Application.Dto;

public sealed record RegisterOutboundOrderCommand(
    string OutboundOrderNumber,
    string ItemCode,
    string WarehouseCode,
    string LocationCode,
    int Quantity,
    DateOnly ScheduledShipDate,
    string CustomerCode);
