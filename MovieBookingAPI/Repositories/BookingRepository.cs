using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.DTOs;
using Newtonsoft.Json; 
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBookingAsync(int userId, int showtimeId, List<int> seatIds)
        {
            // 1. Chuyển đổi danh sách ghế sang chuỗi JSON (VD: "[1, 2, 3]")
            string seatIdsJson = JsonConvert.SerializeObject(seatIds);

            // 2. Định nghĩa tham số
            var pUserId = new SqlParameter("@UserId", userId);
            var pShowtimeId = new SqlParameter("@ShowtimeId", showtimeId);
            var pSeatIdsJson = new SqlParameter("@SeatIdsJson", seatIdsJson);

            // 3. Thực thi Stored Procedure
            // Sử dụng SqlQueryRaw để lấy kết quả trả về là BookingId (Scalar)
            // Lưu ý: EF Core 8 hỗ trợ map trực tiếp scalar result
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "EXEC usp_CreateBookingTransaction @UserId, @ShowtimeId, @SeatIdsJson",
                    pUserId, pShowtimeId, pSeatIdsJson
                )
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task ConfirmPaymentAsync(int bookingId, int userId, string paymentMethod)
        {
            var pBookingId = new SqlParameter("@BookingId", bookingId);
            var pUserId = new SqlParameter("@UserId", userId);
            var pMethod = new SqlParameter("@PaymentMethod", paymentMethod);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC usp_ConfirmBookingPayment @BookingId, @UserId, @PaymentMethod",
                pBookingId, pUserId, pMethod
            );
        }

        public async Task<List<BookingHistoryRawDto>> GetBookingHistoryAsync(int userId)
        {
            var result = new List<BookingHistoryRawDto>();

            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "usp_GetBookingHistory";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", userId));

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new BookingHistoryRawDto
                        {
                            BookingId = reader.GetInt32(reader.GetOrdinal("BookingId")),
                            BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            Status = reader.GetByte(reader.GetOrdinal("Status")), // TinyInt trong SQL map sang Byte
                            PaymentMethod = reader.IsDBNull(reader.GetOrdinal("PaymentMethod")) ? "" : reader.GetString(reader.GetOrdinal("PaymentMethod")),
                            MovieTitle = reader.GetString(reader.GetOrdinal("MovieTitle")),
                            PosterUrl = reader.IsDBNull(reader.GetOrdinal("PosterUrl")) ? "" : reader.GetString(reader.GetOrdinal("PosterUrl")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            StartTime = reader.GetDateTime(reader.GetOrdinal("StartTime")),
                            CinemaName = reader.GetString(reader.GetOrdinal("CinemaName")),
                            RoomName = reader.GetString(reader.GetOrdinal("RoomName")),
                            SeatName = reader.GetString(reader.GetOrdinal("SeatName"))
                        });
                    }
                }
            }
            return result;
        }
    }
}