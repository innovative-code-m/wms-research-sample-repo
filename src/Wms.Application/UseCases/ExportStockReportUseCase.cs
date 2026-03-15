using System.Text;
using Wms.Application.Dto;
using Wms.Domain.Enums;

namespace Wms.Application.UseCases;

public sealed class ExportStockReportUseCase
{
    private readonly GetStockUseCase getStockUseCase;

    public ExportStockReportUseCase(GetStockUseCase getStockUseCase)
    {
        this.getStockUseCase = getStockUseCase;
    }

    public ExportStockReportResult Execute(ExportStockReportCommand command)
    {
        var stocks = getStockUseCase.Execute(new StockQuery(
            command.ItemCode,
            command.WarehouseCode,
            command.LocationCode));

        var content = command.Format switch
        {
            ReportFormat.Csv => BuildCsv(stocks),
            _ => BuildText(stocks),
        };

        return new ExportStockReportResult(command.Format, stocks.Count, content);
    }

    private static string BuildText(IReadOnlyList<StockView> stocks)
    {
        var builder = new StringBuilder();
        builder.AppendLine("商品コード | 商品名 | 倉庫コード | ロケーションコード | 在庫数量");

        foreach (var stock in stocks)
        {
            builder.AppendLine(
                $"{stock.ItemCode} | {stock.ItemName} | {stock.WarehouseCode} | {stock.LocationCode} | {stock.Quantity}");
        }

        return builder.ToString().TrimEnd();
    }

    private static string BuildCsv(IReadOnlyList<StockView> stocks)
    {
        var builder = new StringBuilder();
        builder.AppendLine("ItemCode,ItemName,WarehouseCode,LocationCode,Quantity");

        foreach (var stock in stocks)
        {
            builder.AppendLine(
                $"{Escape(stock.ItemCode)},{Escape(stock.ItemName)},{Escape(stock.WarehouseCode)},{Escape(stock.LocationCode)},{stock.Quantity}");
        }

        return builder.ToString().TrimEnd();
    }

    private static string Escape(string value)
    {
        if (!value.Contains(',') && !value.Contains('"'))
        {
            return value;
        }

        return $"\"{value.Replace("\"", "\"\"")}\"";
    }
}
