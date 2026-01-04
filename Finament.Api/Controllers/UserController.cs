using Finament.Api.Security;
using Finament.Application.DTOs.Users.Requests;
using Finament.Application.Interfaces.Api;
using Finament.Application.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finament.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IUserContext _userContext;

    public UserController(IUserService service, IUserContext userContext)
    {
        _service = service;
        _userContext = userContext;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
        // return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // TODO: add role admin and adapt the following auth role endpoints
    [Authorize(Roles = "Admin")] // not working for now
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return Ok(result);
    }

    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
    
    // === 'Me' User ====
    
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = _userContext.UserId;
        var result = await _service.GetByIdAsync(userId);
        return Ok(result);
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe(UpdateUserDto dto)
    {
        var userId = _userContext.UserId;
        var result = await _service.UpdateAsync(userId, dto);
        return Ok(result);
    }
    
    // [Authorize]
    // [HttpDelete("me")]
    // public async Task<IActionResult> DeleteMe()
    // {
    //     var userId = _userContext.UserId;
    //     await _service.DeleteAsync(userId);
    //     return NoContent();
    // }
}
