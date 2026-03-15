using Wms.Domain.Entities;

namespace Wms.Application.Ports;

public interface IOutboundOrderRepository
{
    void Add(OutboundOrder outboundOrder);

    IReadOnlyList<OutboundOrder> List();
}
