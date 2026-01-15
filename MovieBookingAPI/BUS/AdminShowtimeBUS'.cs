using MovieBooking.Domain.DTOs;
using MovieBookingAPI.DAO;

namespace MovieBookingAPI.BUS
{
    public class AdminShowtimeBUS : IAdminShowtimeBUS
    {
        private readonly IAdminShowtimeDAO _adminShowtimeDAO;

        public AdminShowtimeBUS(IAdminShowtimeDAO adminShowtimeDAO)
        {
            _adminShowtimeDAO = adminShowtimeDAO;
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetAllShowtimesAsync()
        {
            return await _adminShowtimeDAO.GetAllShowtimesAsync();
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date)
        {
            return await _adminShowtimeDAO.GetShowtimesByDateAsync(date);
        }

        public async Task<bool> CreateShowtimeAsync(CreateShowtimeRequestDTO req)
        {
            // Validate logic nghiệp vụ: Giờ chiếu không được nhỏ hơn hiện tại
            if (req.StartTime < DateTime.Now) return false;

            return await _adminShowtimeDAO.CreateShowtimeAsync(req);
        }

        public async Task<bool> UpdateShowtimeAsync(int id, UpdateShowtimeRequestDTO req)
        {
            return await _adminShowtimeDAO.UpdateShowtimeAsync(id, req);
        }

        public async Task<bool> DeleteShowtimeAsync(int id)
        {
            return await _adminShowtimeDAO.DeleteShowtimeAsync(id);
        }
    }
}