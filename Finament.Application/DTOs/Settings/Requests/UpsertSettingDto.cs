namespace Finament.Application.DTOs.Settings.Requests;

public class UpsertSettingDto
{
    public int UserId { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int CycleStartDay { get; set; }
}