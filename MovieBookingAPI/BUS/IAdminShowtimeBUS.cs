using MovieBooking.Domain.DTOs;

namespace MovieBookingAPI.BUS
{
    public interface IAdminShowtimeBUS
    {
        Task<IEnumerable<ShowtimeDTO>> GetAllShowtimesAsync();
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date);

        // Sửa tham số input thành DTO
        Task<bool> CreateShowtimeAsync(CreateShowtimeRequestDTO req);
        Task<bool> UpdateShowtimeAsync(int id, UpdateShowtimeRequestDTO req);
        Task<bool> DeleteShowtimeAsync(int id);
    }
}