using Wms.Application.Dto;
using Wms.Application.Ports;
using Wms.Domain.Entities;

namespace Wms.Application.UseCases;

public sealed class RegisterInboundUseCase
{
    private readonly IItemRepository itemRepository;
    private readonly IWarehouseRepository warehouseRepository;
    private readonly ILocationRepository locationRepository;
    private readonly IStockRepository stockRepository;
    private readonly IInboundReceiptRepository inboundReceiptRepository;

    public RegisterInboundUseCase(
        IItemRepository itemRepository,
        IWarehouseRepository warehouseRepository,
        ILocationRepository locationRepository,
        IStockRepository stockRepository,
        IInboundReceiptRepository inboundReceiptRepository)
    {
        this.itemRepository = itemRepository;
        this.warehouseRepository = warehouseRepository;
        this.locationRepository = locationRepository;
        this.stockRepository = stockRepository;
        this.inboundReceiptRepository = inboundReceiptRepository;
    }

    public RegisterInboundResult Execute(RegisterInboundCommand command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command.InboundNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.ItemCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.WarehouseCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.LocationCode);

        if (command.Quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(command.Quantity), "Inbound quantity must be greater than zero.");
        }

        var item = itemRepository.FindByCode(command.ItemCode)
            ?? throw new InvalidOperationException($"Item '{command.ItemCode}' does not exist.");

        var warehouse = warehouseRepository.FindByCode(command.WarehouseCode)
            ?? throw new InvalidOperationException($"Warehouse '{command.WarehouseCode}' does not exist.");

        var location = locationRepository.Find(command.WarehouseCode, command.LocationCode)
            ?? throw new InvalidOperationException($"Location '{command.LocationCode}' does not exist.");

        var inboundReceipt = new InboundReceipt(
            command.InboundNumber,
            item.ItemCode,
            warehouse.WarehouseCode,
            location.LocationCode,
            command.Quantity,
            command.InboundDate);

        inboundReceiptRepository.Add(inboundReceipt);

        var currentStock = stockRepository.FindSingle(item.ItemCode, warehouse.WarehouseCode, location.LocationCode);
        var updatedStock = currentStock is null
            ? new Stock(item.ItemCode, warehouse.WarehouseCode, location.LocationCode, command.Quantity)
            : currentStock.Increase(command.Quantity);

        stockRepository.Save(updatedStock);

        return new RegisterInboundResult(
            inboundReceipt.InboundNumber,
            updatedStock.ItemCode,
            updatedStock.WarehouseCode,
            updatedStock.LocationCode,
            inboundReceipt.Quantity,
            updatedStock.Quantity);
    }
}
