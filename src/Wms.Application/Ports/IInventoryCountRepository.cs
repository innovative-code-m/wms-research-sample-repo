using Wms.Domain.Entities;

namespace Wms.Application.Ports;

public interface IInventoryCountRepository
{
    void Add(InventoryCount inventoryCount);

    IReadOnlyList<InventoryCount> List(DateOnly countedDate);
}
