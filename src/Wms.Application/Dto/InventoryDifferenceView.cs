namespace Wms.Application.Dto;

public sealed record InventoryDifferenceView(
    string InventoryCountId,
    string ItemCode,
    string ItemName,
    string WarehouseCode,
    string LocationCode,
    int BookQuantity,
    int CountedQuantity,
    int DifferenceQuantity);
