using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.Services
{
    public interface IAdminShowtimeService
    {
        Task<int> CreateShowtimeAsync(CreateShowtimeRequestDto request);

        Task UpdateShowtimeAsync(int showtimeId, UpdateShowtimeRequestDto request);

        Task DeleteShowtimeAsync(int showtimeId);

    }
}
