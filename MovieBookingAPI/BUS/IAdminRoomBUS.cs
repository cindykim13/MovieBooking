using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.BUS
{
    public interface IAdminRoomBUS
    {
        Task<int> CreateRoomAsync(CreateRoomRequestDTO request);
        Task DeleteRoomAsync(int roomId);
    }
}
