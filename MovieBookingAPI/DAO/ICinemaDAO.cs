using MovieBooking.Domain.DTOs; // Sử dụng DTO từ project Shared
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public interface ICinemaDAO
    {
        Task<List<CinemaDTO>> GetAllCinemasAsync();
    }
}