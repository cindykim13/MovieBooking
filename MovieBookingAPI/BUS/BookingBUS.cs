using MovieBookingAPI.DAO;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.BUS;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS { 
    public class BookingService : IBookingBUS
    {
        private readonly IBookingDAO _bookingDAO;

        public BookingService(IBookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }

        public async Task<(bool Success, string Message, int BookingId)> CreateBookingAsync(int userId, CreateBookingRequestDTO request)
        {
            try
            {
                int bookingId = await _bookingDAO.CreateBookingAsync(userId, request.ShowtimeId, request.SeatIds);

                if (bookingId > 0)
                {
                    return (true, "Đặt vé thành công. Vui lòng thanh toán.", bookingId);
                }

                return (false, "Không thể tạo đơn hàng do lỗi không xác định.", 0);
            }
            catch (NpgsqlException ex)
            {
                // Bắt các mã lỗi nghiệp vụ (SQLSTATE) được RAISE từ Function

                // Mã lỗi 'P0001': Trùng ghế (đã định nghĩa trong usp_CreateBookingTransaction)
                if (ex.SqlState == "P0001")
                {
                    return (false, "Một hoặc nhiều ghế bạn chọn đã có người đặt. Vui lòng chọn ghế khác.", 0);
                }

                // Các mã lỗi nghiệp vụ khác có thể thêm vào đây

                // Nếu không phải lỗi nghiệp vụ đã biết, ném ra để Controller xử lý thành HTTP 500
                throw new Exception("Lỗi cơ sở dữ liệu khi tạo đơn hàng.", ex);
            }
        }

        public async Task<(bool Success, string Message)> ProcessPaymentAsync(int userId, PaymentRequestDTO request)
        {
            try
            {
                await _bookingDAO.ConfirmPaymentAsync(request.BookingId, userId, request.PaymentMethod);
                return (true, "Thanh toán thành công. Vé đã được xác nhận.");
            }
            catch (NpgsqlException ex)
            {
                // Xử lý các mã lỗi nghiệp vụ từ Function usp_ConfirmBookingPayment
                switch (ex.SqlState)
                {
                    case "P0002": return (false, "Đơn hàng không tồn tại.");
                    case "P0003": return (false, "Bạn không có quyền truy cập đơn hàng này.");
                    case "P0004": return (false, "Đơn hàng này đã được thanh toán trước đó.");
                    case "P0005": return (false, "Đơn hàng đã bị hủy.");
                    case "P0006": return (false, "Giao dịch đã hết hạn thanh toán (Quá 10 phút). Vui lòng đặt lại.");
                    default:
                        // Nếu là lỗi khác, ném ra để Controller xử lý HTTP 500
                        throw new Exception("Lỗi cơ sở dữ liệu khi xác nhận thanh toán.", ex);
                }
            }
        }

        public async Task<List<BookingHistoryDTO>> GetBookingHistoryAsync(int userId)
        {
            // Lấy dữ liệu thô từ tầng DAO
            var rawData = await _bookingDAO.GetBookingHistoryAsync(userId);

            // Gom nhóm dữ liệu phẳng thành cấu trúc phân cấp
            var result = rawData
                .GroupBy(x => x.BookingId)
                .Select(g => new BookingHistoryDTO
                {
                    BookingId = g.Key,
                    BookingDate = g.First().BookingDate,
                    TotalAmount = g.First().TotalAmount,
                    Status = GetStatusString(g.First().Status),
                    PaymentMethod = g.First().PaymentMethod,
                    MovieTitle = g.First().MovieTitle,
                    PosterUrl = g.First().PosterUrl,
                    CinemaName = g.First().CinemaName,
                    RoomName = g.First().RoomName,
                    ShowTime = g.First().StartTime,
                    // Gom tất cả SeatName của nhóm này vào một danh sách
                    Seats = g.Select(x => x.SeatName).ToList()
                })
                .OrderByDescending(x => x.BookingDate)
                .ToList();

            return result;
        }

        // Hàm phụ trợ chuyển đổi mã trạng thái sang chuỗi hiển thị
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