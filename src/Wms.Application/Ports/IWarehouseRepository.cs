using Wms.Domain.Entities;

namespace Wms.Application.Ports;

public interface IWarehouseRepository
{
    Warehouse? FindByCode(string warehouseCode);
}
