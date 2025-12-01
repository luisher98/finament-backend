using Finament.Api.Persistence;
using Finament.Application.DTOs.Expenses.Requests;
using Finament.Application.Mapping;
using Finament.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finament.Api.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpenseController : ControllerBase
{
    private readonly FinamentDbContext _db;

    public ExpenseController(FinamentDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int userId)
    {
        var expenses = await _db.Expenses
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.Date)
            .ToListAsync();
        
        var dtos = expenses.Select(ExpenseMapping.ToDto).ToList();

        return Ok(dtos);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateExpenseDto dto)
    {
        if (dto.Amount <= 0)
            return BadRequest(new { message = "Amount must be greater than zero." });
        
        var category = await _db.Categories.FindAsync(dto.CategoryId);
        
        if (category == null)
            return BadRequest(new { message = "Category does not exist." });
        
        // category belongs to user
        if (category.UserId != dto.UserId)
            return BadRequest(new { message = "Category does not belong to the user." });
        
        dto.Tag = ToCamelCase(dto.Tag);
        
        var expense = ExpenseMapping.ToEntity(dto);
        
        _db.Expenses.Add(expense);
        await _db.SaveChangesAsync();
        
        return Ok(ExpenseMapping.ToDto(expense));
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateExpenseDto dto)
    {
        var expense = await _db.Expenses.FindAsync(id);
        if (expense == null)
            return NotFound(new { message = "Expense not found." });

        if (dto.CategoryId.HasValue)
        {
            var category = await _db.Categories.FindAsync(dto.CategoryId.Value);
            if (category == null)
                return BadRequest(new { message = "Category does not exist." });

            if (category.UserId != expense.UserId)
                return BadRequest(new { message = "Category does not belong to this user." });
        }

        if (dto.Tag != null)
            dto.Tag = ToCamelCase(dto.Tag);

        ExpenseMapping.UpdateEntity(expense, dto);

        await _db.SaveChangesAsync();
        return Ok(ExpenseMapping.ToDto(expense));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var expense = await _db.Expenses.FindAsync(id);
        if (expense == null)
            return NotFound(new { message = "Expense not found." });
        
        _db.Expenses.Remove(expense);
        await _db.SaveChangesAsync();
        
        return NoContent();
    }

    
    private string? ToCamelCase(string? tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
            return null;

        tag = tag.Trim();

        var noSpaces = string.Concat(tag.Split(' '));
        return char.ToLower(noSpaces[0]) + noSpaces.Substring(1);
    }
    
}