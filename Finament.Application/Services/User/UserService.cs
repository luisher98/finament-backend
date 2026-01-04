
using Finament.Application.Exceptions;
using Finament.Application.DTOs.Users.Requests;
using Finament.Application.DTOs.Users;
using Finament.Application.Infrastructure;
using Finament.Application.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Finament.Application.Services.User;

public sealed class UserService : IUserService
{
    private readonly IFinamentDbContext _db;

    public UserService(IFinamentDbContext db)
    {
        _db = db;
    }    
    
    public async Task<IReadOnlyList<UserResponseDto>> GetAllAsync()
    {
        var users = await _db.Users.ToListAsync();
        return users.Select(UserMapping.ToDto).ToList();
    }    
    
    public async Task<UserResponseDto> GetByIdAsync(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null)
            throw new NotFoundException("User not found.");

        return UserMapping.ToDto(user);
    } 
    
    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        var duplicate = await _db.Users
            .AnyAsync(u => u.Email == dto.Email);

        if (duplicate)
            throw new BusinessRuleException("Email already exists.");

        var user = UserMapping.ToEntity(dto, null);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return UserMapping.ToDto(user);
    }    
    
    public async Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null)
            throw new NotFoundException("User not found.");

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            var duplicate = await _db.Users
                .AnyAsync(u => u.Email == dto.Email && u.Id != id);

            if (duplicate)
                throw new BusinessRuleException("Email already exists.");
        }

        UserMapping.UpdateEntity(user, dto);

        await _db.SaveChangesAsync();

        return UserMapping.ToDto(user);
    }    
    
    public async Task DeleteAsync(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null)
            throw new NotFoundException("User not found.");

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}
