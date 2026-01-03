using MovieBooking.Domain.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public interface IAuthDAO
    {
        Task<int> RegisterUserAsync(string username, string passwordHash, string email, string fullName, string phoneNumber);
        Task<UserAuthData?> GetUserByUsernameAsync(string username);
    }
}