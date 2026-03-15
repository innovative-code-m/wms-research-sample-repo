namespace Wms.Application.Dto;

public sealed record StockView(
    string ItemCode,
    string ItemName,
    string WarehouseCode,
    string LocationCode,
    int Quantity);
