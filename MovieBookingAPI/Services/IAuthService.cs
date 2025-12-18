using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, int UserId)> RegisterAsync(RegisterRequestDto request);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}