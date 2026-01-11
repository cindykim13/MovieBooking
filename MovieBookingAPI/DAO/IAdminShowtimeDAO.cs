using MovieBooking.Domain.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.DAO
{
    public interface IAdminShowtimeDAO
    {
        Task<int> CreateShowtimeAsync(CreateShowtimeRequestDTO request);
        Task UpdateShowtimeAsync(int showtimeId, UpdateShowtimeRequestDTO request);
        Task<string> DeleteShowtimeAsync(int showtimeId); 
        Task<bool> IsRoomInCinemaAsync(int roomId, int cinemaId);
    }
}
