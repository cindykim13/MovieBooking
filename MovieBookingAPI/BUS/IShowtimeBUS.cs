using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public interface IShowtimeBUS
    {
        Task<List<CinemaShowtimeDTO>> GetShowtimesByMovieAsync(int movieId, DateTime? date);
        Task<List<SeatDTO>> GetSeatMapAsync(int showtimeId);

    }
}