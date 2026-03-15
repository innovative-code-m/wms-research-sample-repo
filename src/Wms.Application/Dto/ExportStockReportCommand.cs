using Wms.Domain.Enums;

namespace Wms.Application.Dto;

public sealed record ExportStockReportCommand(
    ReportFormat Format,
    string? ItemCode = null,
    string? WarehouseCode = null,
    string? LocationCode = null);
