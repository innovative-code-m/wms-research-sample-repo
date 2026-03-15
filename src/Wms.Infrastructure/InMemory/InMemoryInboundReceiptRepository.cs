using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.InMemory;

public sealed class InMemoryInboundReceiptRepository : IInboundReceiptRepository
{
    private readonly List<InboundReceipt> inboundReceipts = [];

    public void Add(InboundReceipt inboundReceipt)
    {
        inboundReceipts.Add(inboundReceipt);
    }

    public IReadOnlyList<InboundReceipt> List()
    {
        return inboundReceipts.ToList();
    }
}
