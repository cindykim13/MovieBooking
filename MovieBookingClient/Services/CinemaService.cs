using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Domain.DTOs;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class CinemaService : BaseApiService
    {
        public CinemaService() : base() { }

        public async Task<List<CinemaDTO>> GetAllCinemasAsync()
        {
            // Tạo request đến API lấy danh sách rạp
            var request = CreateRequest("/api/cinemas", Method.Get);

            // Gọi và chờ kết quả
            return await ExecuteAsync<List<CinemaDTO>>(request);
        }
        public async Task<List<RoomDTO>> GetRoomsByCinemaAsync(int cinemaId)
        {
            var request = CreateRequest($"/api/cinemas/{cinemaId}/rooms", Method.Get);
            return await ExecuteAsync<List<RoomDTO>>(request);
        }
    }
}