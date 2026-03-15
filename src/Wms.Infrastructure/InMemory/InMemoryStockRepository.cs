using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryStockRepository : IStockRepository
{
    private readonly List<Stock> stocks;

    public InMemoryStockRepository(IEnumerable<Stock> stocks)
    {
        this.stocks = stocks.ToList();
    }

    public IReadOnlyList<Stock> Find(string? itemCode = null, string? warehouseCode = null, string? locationCode = null)
    {
        return stocks
            .Where(stock => itemCode is null || stock.ItemCode.Equals(itemCode, StringComparison.OrdinalIgnoreCase))
            .Where(stock => warehouseCode is null || stock.WarehouseCode.Equals(warehouseCode, StringComparison.OrdinalIgnoreCase))
            .Where(stock => locationCode is null || stock.LocationCode.Equals(locationCode, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Stock? FindSingle(string itemCode, string warehouseCode, string locationCode)
    {
        return stocks.FirstOrDefault(stock =>
            stock.ItemCode.Equals(itemCode, StringComparison.OrdinalIgnoreCase)
            && stock.WarehouseCode.Equals(warehouseCode, StringComparison.OrdinalIgnoreCase)
            && stock.LocationCode.Equals(locationCode, StringComparison.OrdinalIgnoreCase));
    }

    public void Save(Stock stock)
    {
        var existingIndex = stocks.FindIndex(existing =>
            existing.ItemCode.Equals(stock.ItemCode, StringComparison.OrdinalIgnoreCase)
            && existing.WarehouseCode.Equals(stock.WarehouseCode, StringComparison.OrdinalIgnoreCase)
            && existing.LocationCode.Equals(stock.LocationCode, StringComparison.OrdinalIgnoreCase));

        if (existingIndex >= 0)
        {
            stocks[existingIndex] = stock;
            return;
        }

        stocks.Add(stock);
    }
}
