using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public interface ICinemaBUS
    {
        Task<List<CinemaDTO>> GetAllCinemasAsync();
        Task<List<RoomDTO>> GetRoomsAsync(int? cinemaId);
    }
}