namespace Wms.Application.Dto;

public sealed record RegisterOutboundOrderResult(
    string OutboundOrderNumber,
    string ItemCode,
    string WarehouseCode,
    string LocationCode,
    int OrderedQuantity,
    string CustomerCode,
    string Status);
