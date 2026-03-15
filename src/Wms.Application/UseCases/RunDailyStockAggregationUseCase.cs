using Wms.Application.Dto;
using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Application.UseCases;

public sealed class RunDailyStockAggregationUseCase
{
    private readonly IStockRepository stockRepository;
    private readonly IItemRepository itemRepository;
    private readonly IDailyStockSnapshotRepository dailyStockSnapshotRepository;

    public RunDailyStockAggregationUseCase(
        IStockRepository stockRepository,
        IItemRepository itemRepository,
        IDailyStockSnapshotRepository dailyStockSnapshotRepository)
    {
        this.stockRepository = stockRepository;
        this.itemRepository = itemRepository;
        this.dailyStockSnapshotRepository = dailyStockSnapshotRepository;
    }

    public RunDailyStockAggregationResult Execute(RunDailyStockAggregationCommand command)
    {
        var snapshots = stockRepository.Find()
            .OrderBy(stock => stock.ItemCode)
            .ThenBy(stock => stock.WarehouseCode)
            .ThenBy(stock => stock.LocationCode)
            .Select(stock => new DailyStockSnapshot(
                command.ExecutionDate,
                stock.ItemCode,
                stock.WarehouseCode,
                stock.LocationCode,
                stock.Quantity))
            .ToList();

        dailyStockSnapshotRepository.SaveAll(command.ExecutionDate, snapshots);

        var snapshotViews = snapshots
            .Select(snapshot =>
            {
                var item = itemRepository.FindByCode(snapshot.ItemCode);
                var itemName = item?.ItemName ?? "(unknown item)";

                return new DailyStockSnapshotView(
                    snapshot.SnapshotDate,
                    snapshot.ItemCode,
                    itemName,
                    snapshot.WarehouseCode,
                    snapshot.LocationCode,
                    snapshot.Quantity);
            })
            .ToList();

        var executionLog = $"Daily stock aggregation completed for {command.ExecutionDate:yyyy-MM-dd}. Snapshot count: {snapshotViews.Count}.";

        return new RunDailyStockAggregationResult(
            command.ExecutionDate,
            snapshotViews.Count,
            executionLog,
            snapshotViews);
    }
}
