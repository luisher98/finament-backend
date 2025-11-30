namespace Finament.Application.DTOs.Categories.Requests;

public class CreateCategoryDto
{
    public int UserId { get; set; }
    public string Name { get; set; } = "";
    public decimal MonthlyLimit { get; set; }
    public string Color { get; set; } = "";
}