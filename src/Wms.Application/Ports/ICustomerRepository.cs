using Wms.Domain.Entities;

namespace Wms.Application.Ports;

public interface ICustomerRepository
{
    Customer? FindByCode(string customerCode);
}
