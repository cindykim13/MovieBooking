using Microsoft.Data.SqlClient;
using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace MovieBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;

        public BookingService(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<(bool Success, string Message, int BookingId)> CreateBookingAsync(int userId, CreateBookingRequestDto request)
        {
            try
            {
                int bookingId = await _bookingRepo.CreateBookingAsync(userId, request.ShowtimeId, request.SeatIds);

                if (bookingId > 0)
                {
                    return (true, "Đặt vé thành công. Vui lòng thanh toán.", bookingId);
                }

                return (false, "Không thể tạo đơn hàng.", 0);
            }
            catch (SqlException ex)
            {
                // Bắt các lỗi nghiệp vụ được THROW từ Stored Procedure
                // Error 51001: Trùng ghế
                if (ex.Number == 51001)
                {
                    return (false, "Ghế bạn chọn đã có người đặt. Vui lòng chọn ghế khác.", 0);
                }
                // Error 51002: Dữ liệu không hợp lệ
                if (ex.Number == 51002)
                {
                    return (false, "Dữ liệu suất chiếu hoặc ghế không hợp lệ.", 0);
                }

                // Các lỗi hệ thống khác -> Ném ra để Controller xử lý 500
                throw;
            }
        }

        public async Task<(bool Success, string Message)> ProcessPaymentAsync(int userId, PaymentRequestDto request)
        {
            try
            {
                // Có thể thêm logic gọi cổng thanh toán thật (Stripe/Momo) tại đây.
                // Trong đồ án này, ta giả lập thanh toán luôn thành công và gọi DB cập nhật.

                await _bookingRepo.ConfirmPaymentAsync(request.BookingId, userId, request.PaymentMethod);

                return (true, "Thanh toán thành công. Vé đã được xác nhận.");
            }
            catch (SqlException ex)
            {
                // Xử lý các mã lỗi nghiệp vụ từ Stored Procedure
                switch (ex.Number)
                {
                    case 52000: return (false, "Đơn hàng không tồn tại.");
                    case 52001: return (false, "Bạn không có quyền truy cập đơn hàng này.");
                    case 52002: return (false, "Đơn hàng đã được thanh toán trước đó.");
                    case 52003: return (false, "Đơn hàng đã bị hủy.");
                    case 52004: return (false, "Giao dịch đã hết hạn thanh toán (Quá 10 phút). Vui lòng đặt lại.");
                    default: throw; // Lỗi hệ thống khác
                }
            }
        }

        public async Task<List<BookingHistoryDto>> GetBookingHistoryAsync(int userId)
        {
            // 1. Lấy dữ liệu thô
            var rawData = await _bookingRepo.GetBookingHistoryAsync(userId);

            // 2. Gom nhóm theo BookingId
            var result = rawData
                .GroupBy(x => x.BookingId)
                .Select(g => new BookingHistoryDto
                {
                    BookingId = g.Key,
                    BookingDate = g.First().BookingDate,
                    TotalAmount = g.First().TotalAmount,
                    // Chuyển đổi trạng thái sang chuỗi dễ đọc
                    Status = GetStatusString(g.First().Status),
                    PaymentMethod = g.First().PaymentMethod,
                    MovieTitle = g.First().MovieTitle,
                    PosterUrl = g.First().PosterUrl,
                    CinemaName = g.First().CinemaName,
                    RoomName = g.First().RoomName,
                    ShowTime = g.First().StartTime,
                    // Gom tất cả SeatName của nhóm này vào list
                    Seats = g.Select(x => x.SeatName).ToList()
                })
                .OrderByDescending(x => x.BookingDate)
                .ToList();

            return result;
        }

        // Hàm phụ trợ chuyển đổi Status
        private string GetStatusString(int status)
        {
            return status switch
            {
                1 => "Đã thanh toán",
                2 => "Đã hủy",
                _ => "Chờ thanh toán"
            };
        }
    }
}