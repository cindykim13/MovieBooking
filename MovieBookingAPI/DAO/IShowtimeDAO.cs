using MovieBooking.Domain.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public interface IShowtimeDAO
    {
        Task<List<ShowtimeRawDTO>> GetRawShowtimesAsync(int movieId, DateTime date);
        Task<List<SeatDTO>> GetSeatMapAsync(int showtimeId);
    }
}