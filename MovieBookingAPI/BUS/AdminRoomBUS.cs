using MovieBooking.Domain.DTOs;
using MovieBookingAPI.DAO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public class AdminRoomBUS : IAdminRoomBUS
    {
        private readonly IAdminRoomDAO _adminRoomDAO;

        public AdminRoomBUS(IAdminRoomDAO adminRoomDAO)
        {
            _adminRoomDAO = adminRoomDAO;
        }

        public async Task<IEnumerable<RoomDTO>> GetAllRoomsAsync()
        {
            return await _adminRoomDAO.GetAllRoomsAsync();
        }

        // Nhận vào CreateRoomRequestDTO và truyền xuống DAO
        public async Task<bool> CreateRoomAsync(CreateRoomRequestDTO dto)
        {
            return await _adminRoomDAO.CreateRoomAsync(dto);
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            return await _adminRoomDAO.DeleteRoomAsync(id);
        }
    }
}