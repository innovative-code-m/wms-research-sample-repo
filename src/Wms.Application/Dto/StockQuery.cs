namespace Wms.Application.Dto;

public sealed record StockQuery(
    string? ItemCode = null,
    string? WarehouseCode = null,
    string? LocationCode = null);
