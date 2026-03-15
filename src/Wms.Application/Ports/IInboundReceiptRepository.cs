using Wms.Domain.Entities;

namespace Wms.Application.Ports;

public interface IInboundReceiptRepository
{
    void Add(InboundReceipt inboundReceipt);

    IReadOnlyList<InboundReceipt> List();
}
