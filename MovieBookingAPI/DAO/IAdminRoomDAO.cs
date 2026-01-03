using MovieBooking.Domain.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.DAO
{
    public interface IAdminRoomDAO
    {
        Task<int> CreateRoomWithSeatsAsync(CreateRoomRequestDTO request);
        Task DeleteRoomAsync(int roomId);

    }
}

