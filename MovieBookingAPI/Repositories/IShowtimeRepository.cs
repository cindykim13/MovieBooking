using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.Repositories
{
    public interface IShowtimeRepository
    {
        Task<List<ShowtimeRawDto>> GetRawShowtimesAsync(int movieId, DateTime date);
        Task<List<SeatDto>> GetSeatMapAsync(int showtimeId);
    }
}