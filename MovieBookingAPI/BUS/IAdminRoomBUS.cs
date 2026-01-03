using MovieBooking.Domain.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.BUS
{
    public interface IAdminRoomBUS
    {
        Task<int> CreateRoomAsync(CreateRoomRequestDTO request);
        Task DeleteRoomAsync(int roomId);
    }
}
