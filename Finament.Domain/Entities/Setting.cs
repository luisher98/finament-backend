namespace Finament.Domain.Entities;

public class Setting
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Currency { get; set; } = string.Empty;

    public int CycleStartDay { get; set; }
}
