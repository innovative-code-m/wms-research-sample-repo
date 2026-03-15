using Wms.Domain.Entities;

namespace Wms.Application.Ports;

public interface ILocationRepository
{
    Location? Find(string warehouseCode, string locationCode);
}
