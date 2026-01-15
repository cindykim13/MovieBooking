using MovieBooking.Domain.DTOs;

namespace MovieBookingAPI.DAO
{
    public interface IAdminShowtimeDAO
    {
        Task<IEnumerable<ShowtimeDTO>> GetAllShowtimesAsync();
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date);
        Task<bool> CreateShowtimeAsync(CreateShowtimeRequestDTO req);
        Task<bool> UpdateShowtimeAsync(int id, UpdateShowtimeRequestDTO req);
        Task<bool> DeleteShowtimeAsync(int id);
    }
}