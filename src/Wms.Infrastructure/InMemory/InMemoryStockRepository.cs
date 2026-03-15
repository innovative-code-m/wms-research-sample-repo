using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryStockRepository : IStockRepository
{
    private readonly IReadOnlyList<Stock> stocks;

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
}
