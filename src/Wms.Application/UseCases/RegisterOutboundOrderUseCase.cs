using Wms.Application.Dto;
using Wms.Application.Ports;
using Wms.Domain.Entities;
using Wms.Domain.Enums;

namespace Wms.Application.UseCases;

public sealed class RegisterOutboundOrderUseCase
{
    private readonly IItemRepository itemRepository;
    private readonly IWarehouseRepository warehouseRepository;
    private readonly ILocationRepository locationRepository;
    private readonly ICustomerRepository customerRepository;
    private readonly IOutboundOrderRepository outboundOrderRepository;

    public RegisterOutboundOrderUseCase(
        IItemRepository itemRepository,
        IWarehouseRepository warehouseRepository,
        ILocationRepository locationRepository,
        ICustomerRepository customerRepository,
        IOutboundOrderRepository outboundOrderRepository)
    {
        this.itemRepository = itemRepository;
        this.warehouseRepository = warehouseRepository;
        this.locationRepository = locationRepository;
        this.customerRepository = customerRepository;
        this.outboundOrderRepository = outboundOrderRepository;
    }

    public RegisterOutboundOrderResult Execute(RegisterOutboundOrderCommand command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command.OutboundOrderNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.ItemCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.WarehouseCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.LocationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.CustomerCode);

        if (command.Quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(command.Quantity), "Outbound quantity must be greater than zero.");
        }

        var item = itemRepository.FindByCode(command.ItemCode)
            ?? throw new InvalidOperationException($"Item '{command.ItemCode}' does not exist.");

        var warehouse = warehouseRepository.FindByCode(command.WarehouseCode)
            ?? throw new InvalidOperationException($"Warehouse '{command.WarehouseCode}' does not exist.");

        var location = locationRepository.Find(command.WarehouseCode, command.LocationCode)
            ?? throw new InvalidOperationException($"Location '{command.LocationCode}' does not exist.");

        var customer = customerRepository.FindByCode(command.CustomerCode)
            ?? throw new InvalidOperationException($"Customer '{command.CustomerCode}' does not exist.");

        var outboundOrder = new OutboundOrder(
            command.OutboundOrderNumber,
            item.ItemCode,
            warehouse.WarehouseCode,
            location.LocationCode,
            command.Quantity,
            command.ScheduledShipDate,
            customer.CustomerCode,
            OutboundOrderStatus.Planned);

        outboundOrderRepository.Add(outboundOrder);

        return new RegisterOutboundOrderResult(
            outboundOrder.OutboundOrderNumber,
            outboundOrder.ItemCode,
            outboundOrder.WarehouseCode,
            outboundOrder.LocationCode,
            outboundOrder.Quantity,
            outboundOrder.CustomerCode,
            outboundOrder.Status.ToString());
    }
}
