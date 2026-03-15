using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryWarehouseRepository : IWarehouseRepository
{
    private readonly Dictionary<string, Warehouse> warehousesByCode;

    public InMemoryWarehouseRepository(IEnumerable<Warehouse> warehouses)
    {
        warehousesByCode = warehouses.ToDictionary(warehouse => warehouse.WarehouseCode, StringComparer.OrdinalIgnoreCase);
    }

    public Warehouse? FindByCode(string warehouseCode)
    {
        warehousesByCode.TryGetValue(warehouseCode, out var warehouse);
        return warehouse;
    }
}
