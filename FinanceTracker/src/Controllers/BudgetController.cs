using FinanceTracker.DTOs;
using FinanceTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers;

[ApiController]
[Route("budgets")]
public class BudgetController : ControllerBase
{
    private readonly BudgetService _budgetService;

    public BudgetController(BudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    [HttpPost]
    public IActionResult SetBudget([FromBody] CreateBudgetRequest request)
    {
        var budget = _budgetService.SetBudget(request);
        return Created(string.Empty, budget);
    }

    [HttpGet]
    public IActionResult GetBudgets([FromQuery] Guid userId)
    {
        var budgetStatuses = _budgetService.GetBudgetStatus(userId);
        return Ok(budgetStatuses);
    }
}
