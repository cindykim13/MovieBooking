using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Domain.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.BUS
{
    public interface IAdminShowtimeBUS
    {
        Task<int> CreateShowtimeAsync(CreateShowtimeRequestDTO request);
        Task UpdateShowtimeAsync(int showtimeId, UpdateShowtimeRequestDTO request);
        Task<string> DeleteShowtimeAsync(int showtimeId);
        Task<ShowtimeDetailDTO?> GetShowtimeDetailAsync(int showtimeId);
    }
}
