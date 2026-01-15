using MovieBooking.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public interface IAdminRoomBUS
    {
        Task<IEnumerable<RoomDTO>> GetAllRoomsAsync();
        // Cập nhật tham số DTO tại đây
        Task<bool> CreateRoomAsync(CreateRoomRequestDTO dto);
        Task<bool> DeleteRoomAsync(int id);
    }
}