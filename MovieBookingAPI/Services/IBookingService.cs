using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.Services
{
    public interface IBookingService
    {
        // Trả về Tuple: (Thành công?, Thông báo, BookingId)
        Task<(bool Success, string Message, int BookingId)> CreateBookingAsync(int userId, CreateBookingRequestDto request);
        Task<(bool Success, string Message)> ProcessPaymentAsync(int userId, PaymentRequestDto request);
        Task<List<BookingHistoryDto>> GetBookingHistoryAsync(int userId);
    }
}