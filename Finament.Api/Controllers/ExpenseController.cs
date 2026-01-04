using Finament.Application.DTOs.Expenses.Requests;
using Finament.Application.Interfaces.Api;
using Finament.Application.Services.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finament.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/expenses")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _service;
    private readonly IUserContext _userContext;

    public ExpenseController(IExpenseService service, IUserContext userContext)
    {
        _service = service;
        _userContext = userContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = _userContext.UserId;
        var result = await _service.GetByUserAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExpenseDto dto)
    {
        var userId = _userContext.UserId;
        var result = await _service.CreateAsync(userId, dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateExpenseDto dto)
    {
        var userId = _userContext.UserId;
        var result = await _service.UpdateAsync(userId, id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}