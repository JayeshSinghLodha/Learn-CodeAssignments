using FinanceTracker.DTOs;
using FinanceTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;

    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public IActionResult AddTransaction([FromBody] CreateTransactionRequest request)
    {
        try
        {
            var transaction = _transactionService.AddTransaction(request);
            return CreatedAtAction(nameof(GetTransactions), new { userId = transaction.UserId }, transaction);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetTransactions(
        [FromQuery] Guid userId,
        [FromQuery] string? category,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to)
    {
        try
        {
            var transactions = _transactionService.GetTransactions(userId, category, from, to);
            return Ok(transactions);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteTransaction(Guid id)
    {
        var deleted = _transactionService.DeleteTransaction(id);
        return deleted ? NoContent() : NotFound();
    }
}
