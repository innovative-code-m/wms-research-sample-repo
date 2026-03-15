using Wms.Application.Dto;
using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Application.UseCases;

public sealed class GenerateInventoryDifferenceReportUseCase
{
    private readonly IInventoryCountRepository inventoryCountRepository;
    private readonly IStockRepository stockRepository;
    private readonly IItemRepository itemRepository;

    public GenerateInventoryDifferenceReportUseCase(
        IInventoryCountRepository inventoryCountRepository,
        IStockRepository stockRepository,
        IItemRepository itemRepository)
    {
        this.inventoryCountRepository = inventoryCountRepository;
        this.stockRepository = stockRepository;
        this.itemRepository = itemRepository;
    }

    public GenerateInventoryDifferenceReportResult Execute(GenerateInventoryDifferenceReportCommand command)
    {
        var inventoryCounts = inventoryCountRepository.List(command.CountedDate)
            .OrderBy(count => count.ItemCode)
            .ThenBy(count => count.WarehouseCode)
            .ThenBy(count => count.LocationCode)
            .ToList();

        var differences = inventoryCounts
            .Select(count =>
            {
                var stock = stockRepository.FindSingle(count.ItemCode, count.WarehouseCode, count.LocationCode);
                var bookQuantity = stock?.Quantity ?? 0;

                return new InventoryDifference(
                    count.InventoryCountId,
                    count.ItemCode,
                    count.WarehouseCode,
                    count.LocationCode,
                    bookQuantity,
                    count.CountedQuantity);
            })
            .ToList();

        var views = differences
            .Select(difference =>
            {
                var item = itemRepository.FindByCode(difference.ItemCode);
                var itemName = item?.ItemName ?? "(unknown item)";

                return new InventoryDifferenceView(
                    difference.InventoryCountId,
                    difference.ItemCode,
                    itemName,
                    difference.WarehouseCode,
                    difference.LocationCode,
                    difference.BookQuantity,
                    difference.CountedQuantity,
                    difference.DifferenceQuantity);
            })
            .ToList();

        var differenceCount = differences.Count(difference => difference.DifferenceQuantity != 0);
        var executionLog =
            $"Inventory difference report generated for {command.CountedDate:yyyy-MM-dd}. Report lines: {views.Count}. Difference lines: {differenceCount}.";

        return new GenerateInventoryDifferenceReportResult(
            command.CountedDate,
            views.Count,
            differenceCount,
            executionLog,
            views);
    }
}
