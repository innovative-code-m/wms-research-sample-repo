using Wms.Domain.Enums;

namespace Wms.Application.Dto;

public sealed record ExportStockReportResult(
    ReportFormat Format,
    int LineCount,
    string Content);
