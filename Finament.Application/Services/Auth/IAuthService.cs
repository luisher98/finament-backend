using Finament.Application.DTOs.Auth;
using Finament.Application.DTOs.Auth.Requests;

namespace Finament.Application.Services.Auth;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
}