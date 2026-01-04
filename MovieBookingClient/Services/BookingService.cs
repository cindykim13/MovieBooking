using MovieBooking.Domain.DTOs;
using RestSharp;
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class BookingService : BaseApiService
    {
        // Gọi API tạo Booking
        public async Task<dynamic> CreateBookingAsync(CreateBookingRequestDTO requestDto)
        {
            var request = CreateRequest("/api/bookings", Method.Post);
            request.AddBody(requestDto);

            // Trả về dynamic để lấy BookingId từ JSON response
            return await ExecuteAsync<dynamic>(request);
        }

        // Gọi API thanh toán (Sẽ dùng ở bước sau)
        public async Task<bool> ConfirmPaymentAsync(int bookingId, string method)
        {
            var request = CreateRequest("/api/bookings/payment", Method.Post);
            request.AddBody(new { bookingId = bookingId, paymentMethod = method });

            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }
    }
}