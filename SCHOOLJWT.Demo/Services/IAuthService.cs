using SCHOOLJWT.Demo.Entities;
using SCHOOLJWT.Demo.Model;


namespace SCHOOLJWT.Demo.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
