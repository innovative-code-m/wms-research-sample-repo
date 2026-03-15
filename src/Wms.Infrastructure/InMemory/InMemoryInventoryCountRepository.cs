using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryInventoryCountRepository : IInventoryCountRepository
{
    private readonly List<InventoryCount> inventoryCounts = [];

    public void Add(InventoryCount inventoryCount)
    {
        inventoryCounts.Add(inventoryCount);
    }

    public IReadOnlyList<InventoryCount> List(DateOnly countedDate)
    {
        return inventoryCounts
            .Where(count => count.CountedDate == countedDate)
            .ToList();
    }
}
