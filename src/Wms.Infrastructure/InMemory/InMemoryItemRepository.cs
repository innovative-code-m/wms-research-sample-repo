using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryItemRepository : IItemRepository
{
    private readonly Dictionary<string, Item> itemsByCode;

    public InMemoryItemRepository(IEnumerable<Item> items)
    {
        itemsByCode = items.ToDictionary(item => item.ItemCode, StringComparer.OrdinalIgnoreCase);
    }

    public Item? FindByCode(string itemCode)
    {
        itemsByCode.TryGetValue(itemCode, out var item);
        return item;
    }
}
