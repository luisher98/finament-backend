using Finament.Application.DTOs.Categories;
using Finament.Application.DTOs.Categories.Requests;

namespace Finament.Application.Services.Category;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryResponseDto>> GetByUserAsync(int userId);
    Task<CategoryResponseDto> CreateAsync(int userId, CreateCategoryDto dto);
    Task<CategoryResponseDto> UpdateAsync(int userId, int id, UpdateCategoryDto dto);
    Task DeleteAsync(int id);
}