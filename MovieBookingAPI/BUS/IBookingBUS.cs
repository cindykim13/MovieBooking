using MovieBooking.Domain.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public interface IBookingBUS
    {
        // Trả về Tuple: (Thành công?, Thông báo, BookingId)
        Task<(bool Success, string Message, int BookingId)> CreateBookingAsync(int userId, CreateBookingRequestDTO request);
        Task<(bool Success, string Message)> ProcessPaymentAsync(int userId, PaymentRequestDTO request);
        Task<List<BookingHistoryDTO>> GetBookingHistoryAsync(int userId);
    }
}