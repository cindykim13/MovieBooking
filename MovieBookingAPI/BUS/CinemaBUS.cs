using MovieBooking.Domain.DTOs;
using MovieBookingAPI.DAO;
using MovieBookingAPI.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public class CinemaBUS : ICinemaBUS
    {
        private readonly ICinemaDAO _cinemaDAO;

        public CinemaBUS(ICinemaDAO cinemaDAO)
        {
            _cinemaDAO = cinemaDAO;
        }

        public async Task<List<CinemaDTO>> GetAllCinemasAsync()
        {
            // Logic nghiệp vụ có thể thêm ở đây, ví dụ: gom nhóm
            return await _cinemaDAO.GetAllCinemasAsync();
        }
        public async Task<List<RoomDTO>> GetRoomsAsync(int? cinemaId)
        {
            return await _cinemaDAO.GetRoomsAsync(cinemaId);
        }
    }
}