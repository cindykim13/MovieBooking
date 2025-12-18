using System.Threading.Tasks;
using MovieBookingAPI.Models.DTOs;

namespace MovieBookingAPI.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetUserProfileAsync(int userId);
        Task<bool> UpdateUserProfileAsync(int userId, UpdateProfileRequestDto request);
        Task<(bool Success, string Message)> ChangePasswordAsync(int userId, ChangePasswordRequestDto request);
    }
}