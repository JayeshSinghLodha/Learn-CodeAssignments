using FinanceTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers;

[ApiController]
[Route("reports")]
public class ReportController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("summary")]
    public IActionResult GetMonthlySummary(
        [FromQuery] Guid userId,
        [FromQuery] int month,
        [FromQuery] int year)
    {
        var summary = _reportService.GetMonthlySummary(userId, month, year);
        return Ok(summary);
    }
}
