using Finament.Application.DTOs.Users;
using Finament.Application.DTOs.Users.Requests;

namespace Finament.Application.Services.User;

public interface IUserService
{
    Task<IReadOnlyList<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto> GetByIdAsync(int id);
    Task<UserResponseDto> CreateAsync(CreateUserDto dto);
    Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto dto);
    Task DeleteAsync(int id);
}