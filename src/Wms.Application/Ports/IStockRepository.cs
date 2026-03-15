using Wms.Domain.Entities;

namespace Wms.Application.Ports;

public interface IStockRepository
{
    IReadOnlyList<Stock> Find(string? itemCode = null, string? warehouseCode = null, string? locationCode = null);
}
