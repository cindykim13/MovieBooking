using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.Repositories
{
    public interface IAuthRepository
    {
        Task<int> RegisterUserAsync(string username, string passwordHash, string email, string fullName, string phoneNumber);
        Task<UserAuthData> GetUserByUsernameAsync(string username);
    }
}