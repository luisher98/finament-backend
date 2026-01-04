using Finament.Application.DTOs.Settings.Requests;
using Finament.Application.Interfaces.Api;
using Finament.Application.Services.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finament.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/settings")]
public class SettingController : ControllerBase
{
    private readonly ISettingService _service;
    private readonly IUserContext _userContext;

    public SettingController(ISettingService service,  IUserContext userContext)
    {
        _service = service;
        _userContext = userContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = _userContext.UserId;
        var result = await _service.GetByUserAsync(userId);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Upsert([FromBody] UpsertSettingDto dto)
    {
        var userId = _userContext.UserId;
        var result = await _service.UpsertAsync(userId, dto);
        return Ok(result);
    }
}