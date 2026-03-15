using Wms.Application.Dto;
using Wms.Application.Ports;

namespace Wms.Application.UseCases;

public sealed class GetStockUseCase
{
    private readonly IStockRepository stockRepository;
    private readonly IItemRepository itemRepository;

    public GetStockUseCase(IStockRepository stockRepository, IItemRepository itemRepository)
    {
        this.stockRepository = stockRepository;
        this.itemRepository = itemRepository;
    }

    public IReadOnlyList<StockView> Execute(StockQuery query)
    {
        var stocks = stockRepository.Find(query.ItemCode, query.WarehouseCode, query.LocationCode);

        return stocks
            .OrderBy(stock => stock.ItemCode)
            .ThenBy(stock => stock.WarehouseCode)
            .ThenBy(stock => stock.LocationCode)
            .Select(stock =>
            {
                var item = itemRepository.FindByCode(stock.ItemCode);
                var itemName = item?.ItemName ?? "(unknown item)";

                return new StockView(
                    stock.ItemCode,
                    itemName,
                    stock.WarehouseCode,
                    stock.LocationCode,
                    stock.Quantity);
            })
            .ToList();
    }
}
