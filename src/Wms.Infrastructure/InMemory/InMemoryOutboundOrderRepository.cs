using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryOutboundOrderRepository : IOutboundOrderRepository
{
    private readonly List<OutboundOrder> outboundOrders = [];

    public void Add(OutboundOrder outboundOrder)
    {
        outboundOrders.Add(outboundOrder);
    }

    public IReadOnlyList<OutboundOrder> List()
    {
        return outboundOrders.ToList();
    }
}
