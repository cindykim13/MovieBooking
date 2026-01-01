using System.Threading.Tasks;
using MovieBookingAPI.Models.DTOs;

namespace MovieBookingAPI.BUS
{
    public interface IUserBUS
    {
        Task<UserProfileDTO?> GetUserProfileAsync(int userId);
        Task<bool> UpdateUserProfileAsync(int userId, UpdateProfileRequestDTO request);
        Task<(bool Success, string Message)> ChangePasswordAsync(int userId, ChangePasswordRequestDTO request);
    }
}