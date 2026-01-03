using MovieBooking.Domain.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public interface IAuthBUS
    {
        Task<(bool Success, string Message, int UserId)> RegisterAsync(RegisterRequestDTO request);
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
    }
}