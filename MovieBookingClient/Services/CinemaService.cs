using MovieBooking.Domain.DTOs;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieBookingClient.UI.Modules;

namespace MovieBookingClient.Services
{
    public class CinemaService : BaseApiService
    {
        public CinemaService() : base() { }

        // Hàm lấy danh sách rạp
        public async Task<List<CinemaDTO>> GetAllCinemasAsync()
        {
            var request = CreateRequest("api/cinemas", Method.Get);
            var result = await ExecuteAsync<List<CinemaDTO>>(request);
            return result ?? new List<CinemaDTO>();
        }
    }
}