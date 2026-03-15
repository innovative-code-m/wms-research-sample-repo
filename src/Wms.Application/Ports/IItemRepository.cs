using Wms.Domain.Entities;

namespace Wms.Application.Ports;

public interface IItemRepository
{
    Item? FindByCode(string itemCode);
}
