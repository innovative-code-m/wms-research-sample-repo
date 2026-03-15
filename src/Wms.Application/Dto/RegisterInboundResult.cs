namespace Wms.Application.Dto;

public sealed record RegisterInboundResult(
    string InboundNumber,
    string ItemCode,
    string WarehouseCode,
    string LocationCode,
    int ReceivedQuantity,
    int UpdatedStockQuantity);
