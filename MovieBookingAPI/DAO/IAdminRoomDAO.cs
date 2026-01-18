using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.DAO
{
    public interface IAdminRoomDAO
    {
        Task<int> CreateRoomWithSeatsAsync(CreateRoomRequestDTO request);
        Task DeleteRoomAsync(int roomId);
        Task<List<RoomTemplateDTO>> GetAllTemplatesAsync();
    }
}

