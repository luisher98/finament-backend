using Finament.Application.DTOs.Settings;
using Finament.Application.DTOs.Settings.Requests;
using Finament.Domain.Entities;

namespace Finament.Application.Mapping;

public static class SettingMapping
{
    public static SettingResponseDto ToDto(Setting setting)
    {
        return new SettingResponseDto
        {
            Id = setting.Id,
            UserId = setting.UserId,
            Currency = setting.Currency,
            CycleStartDay = setting.CycleStartDay,
        };
    }

    public static Setting Create(UpsertSettingDto dto)
    {
        return new Setting
        {
            UserId = dto.UserId,
            Currency = dto.Currency,
            CycleStartDay = dto.CycleStartDay,
        };
    }

    public static void UpdateEntity(Setting setting, UpsertSettingDto dto)
    {
        setting.Currency = dto.Currency;
        setting.CycleStartDay = dto.CycleStartDay;
    }
}