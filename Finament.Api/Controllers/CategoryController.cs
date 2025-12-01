using Finament.Api.Persistence;
using Finament.Application.DTOs.Categories.Requests;
using Finament.Application.Mapping;
using Finament.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finament.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly FinamentDbContext _db;

    public CategoryController(FinamentDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int userId)
    {
        var categories = await _db.Categories
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name)
            .ToListAsync();

        var dtos = categories.Select(CategoryMapping.ToDto).ToList();
        return Ok(dtos);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest(new { message = "Name is required." });
        
        var duplicate = await _db.Categories
            .AnyAsync(c => c.UserId == dto.UserId && c.Name == dto.Name);
        
        if (duplicate)
            return Conflict(new { message = "Category name already exists for this user." });

        var category = CategoryMapping.ToEntity(dto); 
        
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();

        return Ok(CategoryMapping.ToDto(category));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category == null)
            return NotFound(new { message = "Category not found." });
        
        if (dto.Name != null)
        {
            var duplicate = await _db.Categories
                .AnyAsync(c => c.UserId == category.UserId && c.Name == dto.Name && c.Id != id);

            if (duplicate)
                return Conflict(new { message = "Category name already exists for this user." });
        }

        CategoryMapping.UpdateEntity(category, dto);

        await _db.SaveChangesAsync();

        return Ok(CategoryMapping.ToDto(category));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _db.Categories.FindAsync(id);

        if (category == null)
            return NotFound(new { message = "Category not found." });
        
        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
        
        return NoContent();
    }
}