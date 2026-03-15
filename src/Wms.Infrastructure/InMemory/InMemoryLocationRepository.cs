using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryLocationRepository : ILocationRepository
{
    private readonly Dictionary<string, Location> locationsByKey;

    public InMemoryLocationRepository(IEnumerable<Location> locations)
    {
        locationsByKey = locations.ToDictionary(
            location => BuildKey(location.WarehouseCode, location.LocationCode),
            StringComparer.OrdinalIgnoreCase);
    }

    public Location? Find(string warehouseCode, string locationCode)
    {
        locationsByKey.TryGetValue(BuildKey(warehouseCode, locationCode), out var location);
        return location;
    }

    private static string BuildKey(string warehouseCode, string locationCode)
    {
        return $"{warehouseCode}:{locationCode}";
    }
}
