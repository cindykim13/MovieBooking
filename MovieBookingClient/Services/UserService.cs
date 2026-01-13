using MovieBooking.Domain.DTOs;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class UserService : BaseApiService
    {
        public async Task<UserProfileDTO> GetProfileAsync()
        {
            var request = CreateRequest("/api/user/me", Method.Get);
            return await ExecuteAsync<UserProfileDTO>(request);
        }

        public async Task<List<BookingHistoryDTO>> GetBookingHistoryAsync()
        {
            var request = CreateRequest("/api/Bookings/history", Method.Get);
            return await ExecuteAsync<List<BookingHistoryDTO>>(request);
        }
    }
}