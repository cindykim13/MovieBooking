using System.Threading.Tasks;
using MovieBookingAPI.Models.Entities;

namespace MovieBookingAPI.DAO
{
    public interface IUserDAO
    {
        // Lấy thông tin user theo ID
        Task<AppUser?> GetUserByIdAsync(int userId);
        Task<bool> UpdateProfileAsync(int userId, string fullName, string email, string phoneNumber);
        Task<bool> ChangePasswordAsync(int userId, string newPasswordHash);
    }
}