using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly Dictionary<string, Customer> customersByCode;

    public InMemoryCustomerRepository(IEnumerable<Customer> customers)
    {
        customersByCode = customers.ToDictionary(customer => customer.CustomerCode, StringComparer.OrdinalIgnoreCase);
    }

    public Customer? FindByCode(string customerCode)
    {
        customersByCode.TryGetValue(customerCode, out var customer);
        return customer;
    }
}
