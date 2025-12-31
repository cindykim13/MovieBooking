using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.Services
{
    public interface IAdminRoomService
    {
        Task<int> CreateRoomAsync(CreateRoomRequestDto request);

        Task DeleteRoomAsync(int roomId);

    }
}
