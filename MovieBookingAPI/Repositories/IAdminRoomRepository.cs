using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.Repositories
{
    public interface IAdminRoomRepository
    {
        Task<int> CreateRoomWithSeatsAsync(CreateRoomRequestDto request);

        Task DeleteRoomAsync(int roomId);
    }
}
