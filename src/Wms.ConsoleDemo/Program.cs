using Wms.Application.Dto;
using Wms.Application.UseCases;
using Wms.Domain.Entities;
using Wms.Infrastructure.InMemory;

var items = new[]
{
    new Item("ITEM-001", "標準部品A"),
    new Item("ITEM-002", "標準部品B"),
    new Item("ITEM-003", "完成品C"),
};

var warehouses = new[]
{
    new Warehouse("WH-01", "本庫"),
    new Warehouse("WH-02", "第2倉庫"),
};

var customers = new[]
{
    new Customer("CUS-001", "取引先A"),
    new Customer("CUS-002", "取引先B"),
};

var locations = new[]
{
    new Location("LOC-001", "A-01-01", "WH-01"),
    new Location("LOC-002", "A-01-02", "WH-01"),
    new Location("LOC-003", "B-01-01", "WH-01"),
    new Location("LOC-001", "A-01-01", "WH-02"),
};

var stocks = new[]
{
    new Stock("ITEM-001", "WH-01", "LOC-001", 100),
    new Stock("ITEM-001", "WH-01", "LOC-002", 50),
    new Stock("ITEM-002", "WH-01", "LOC-003", 30),
    new Stock("ITEM-003", "WH-02", "LOC-001", 10),
};

var itemRepository = new InMemoryItemRepository(items);
var warehouseRepository = new InMemoryWarehouseRepository(warehouses);
var locationRepository = new InMemoryLocationRepository(locations);
var customerRepository = new InMemoryCustomerRepository(customers);
var stockRepository = new InMemoryStockRepository(stocks);
var inboundReceiptRepository = new InMemoryInboundReceiptRepository();
var outboundOrderRepository = new InMemoryOutboundOrderRepository();
var getStockUseCase = new GetStockUseCase(stockRepository, itemRepository);
var registerInboundUseCase = new RegisterInboundUseCase(
    itemRepository,
    warehouseRepository,
    locationRepository,
    stockRepository,
    inboundReceiptRepository);
var registerOutboundOrderUseCase = new RegisterOutboundOrderUseCase(
    itemRepository,
    warehouseRepository,
    locationRepository,
    customerRepository,
    outboundOrderRepository);

Console.WriteLine("=== WMS Console Demo: 在庫照会 / 入荷登録 / 出荷指示登録 ===");
Console.WriteLine();
Console.WriteLine($"投入済みマスタ: 商品 {items.Length} 件 / 倉庫 {warehouses.Length} 件 / ロケーション {locations.Length} 件 / 出荷先 {customers.Length} 件");
Console.WriteLine();

var exactQuery = new StockQuery(
    ItemCode: "ITEM-001",
    WarehouseCode: "WH-01",
    LocationCode: "LOC-001");

var exactResults = getStockUseCase.Execute(exactQuery);

Console.WriteLine("[単一在庫照会]");
Console.WriteLine("条件: ITEM-001 / WH-01 / LOC-001");

if (exactResults.Count == 0)
{
    Console.WriteLine("結果: 在庫なし (0)");
}
else
{
    var stock = exactResults[0];
    Console.WriteLine($"結果: {stock.ItemCode} {stock.ItemName} / {stock.WarehouseCode} / {stock.LocationCode} / 数量 {stock.Quantity}");
}

Console.WriteLine();

Console.WriteLine("[入荷登録]");
var inboundResult = registerInboundUseCase.Execute(new RegisterInboundCommand(
    InboundNumber: "INB-0001",
    ItemCode: "ITEM-001",
    WarehouseCode: "WH-01",
    LocationCode: "LOC-001",
    Quantity: 20,
    InboundDate: new DateOnly(2026, 3, 16)));

Console.WriteLine(
    $"結果: {inboundResult.InboundNumber} / {inboundResult.ItemCode} / {inboundResult.WarehouseCode} / {inboundResult.LocationCode} / 入荷 {inboundResult.ReceivedQuantity} / 更新後在庫 {inboundResult.UpdatedStockQuantity}");
Console.WriteLine($"入荷実績件数: {inboundReceiptRepository.List().Count}");
Console.WriteLine();

Console.WriteLine("[入荷後の在庫照会]");
var afterInboundResults = getStockUseCase.Execute(exactQuery);

if (afterInboundResults.Count == 0)
{
    Console.WriteLine("結果: 在庫なし (0)");
}
else
{
    var stock = afterInboundResults[0];
    Console.WriteLine($"結果: {stock.ItemCode} {stock.ItemName} / {stock.WarehouseCode} / {stock.LocationCode} / 数量 {stock.Quantity}");
}

Console.WriteLine();

Console.WriteLine("[出荷指示登録]");
var outboundResult = registerOutboundOrderUseCase.Execute(new RegisterOutboundOrderCommand(
    OutboundOrderNumber: "OUT-0001",
    ItemCode: "ITEM-001",
    WarehouseCode: "WH-01",
    LocationCode: "LOC-001",
    Quantity: 15,
    ScheduledShipDate: new DateOnly(2026, 3, 18),
    CustomerCode: "CUS-001"));

Console.WriteLine(
    $"結果: {outboundResult.OutboundOrderNumber} / {outboundResult.ItemCode} / {outboundResult.WarehouseCode} / {outboundResult.LocationCode} / 指示 {outboundResult.OrderedQuantity} / 出荷先 {outboundResult.CustomerCode} / 状態 {outboundResult.Status}");
Console.WriteLine($"出荷指示件数: {outboundOrderRepository.List().Count}");
Console.WriteLine();

Console.WriteLine("[出荷指示登録後の在庫照会]");
var afterOutboundResults = getStockUseCase.Execute(exactQuery);

if (afterOutboundResults.Count == 0)
{
    Console.WriteLine("結果: 在庫なし (0)");
}
else
{
    var stock = afterOutboundResults[0];
    Console.WriteLine($"結果: {stock.ItemCode} {stock.ItemName} / {stock.WarehouseCode} / {stock.LocationCode} / 数量 {stock.Quantity}");
}

Console.WriteLine();

var noMatchResults = getStockUseCase.Execute(new StockQuery(
    ItemCode: "ITEM-001",
    WarehouseCode: "WH-02",
    LocationCode: "LOC-999"));

Console.WriteLine("[存在しない在庫照会]");
Console.WriteLine("条件: ITEM-001 / WH-02 / LOC-999");
Console.WriteLine(noMatchResults.Count == 0 ? "結果: 在庫なし (0)" : "結果: 在庫あり");

Console.WriteLine();
Console.WriteLine("[在庫一覧]");

foreach (var stock in getStockUseCase.Execute(new StockQuery()))
{
    Console.WriteLine($"{stock.ItemCode,-10} {stock.ItemName,-10} {stock.WarehouseCode,-5} {stock.LocationCode,-7} 数量 {stock.Quantity}");
}
