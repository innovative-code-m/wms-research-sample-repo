namespace Wms.Application.Dto;

public sealed record GenerateInventoryDifferenceReportResult(
    DateOnly CountedDate,
    int ReportLineCount,
    int DifferenceCount,
    string ExecutionLog,
    IReadOnlyList<InventoryDifferenceView> Differences);
