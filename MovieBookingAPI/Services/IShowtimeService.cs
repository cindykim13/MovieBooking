using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.Services
{
    public interface IShowtimeService
    {
        Task<List<CinemaShowtimeDto>> GetShowtimesByMovieAsync(int movieId, DateTime? date);
        Task<List<SeatDto>> GetSeatMapAsync(int showtimeId);

    }
}