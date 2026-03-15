namespace Wms.Application.Dto;

public sealed record RegisterInboundCommand(
    string InboundNumber,
    string ItemCode,
    string WarehouseCode,
    string LocationCode,
    int Quantity,
    DateOnly InboundDate);
