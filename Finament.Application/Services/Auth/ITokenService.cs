namespace Finament.Application.Services.Auth;

public interface ITokenService
{
    string CreateToken(int userId);
}