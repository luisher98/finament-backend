namespace Finament.Application.DTOs.Settings.Requests;

public class UpsertSettingDto
{
    public required int UserId { get; set; }
    public required string Currency { get; set; }
    public int CycleStartDay { get; set; } = 1;
}