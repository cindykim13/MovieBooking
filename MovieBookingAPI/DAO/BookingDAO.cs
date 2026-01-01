using Microsoft.Data.SqlClient; // Dùng SqlParameter của Microsoft cho EF Core
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.DTOs;
using Newtonsoft.Json;
using Npgsql; // Vẫn cần NpgsqlParameter cho ADO.NET
using NpgsqlTypes;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public class BookingDAO : IBookingDAO
    {
        private readonly AppDbContext _context;

        public BookingDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBookingAsync(int userId, int showtimeId, List<int> seatIds)
        {
            // 1. Chuyển đổi danh sách ghế sang chuỗi JSON
            string seatIdsJson = JsonConvert.SerializeObject(seatIds);
            int newBookingId = 0;

            // Sử dụng ADO.NET thuần để kiểm soát hoàn toàn việc gọi Function
            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                // 2. Sửa lại CommandText: gọi hàm và truyền tham số
                command.CommandText = "SELECT * FROM usp_createbookingtransaction(@in_userid, @in_showtimeid, @in_seatidsjson)";
                command.CommandType = CommandType.Text; // Quan trọng: Đổi sang CommandType.Text

                // 3. Thêm tham số
                command.Parameters.Add(new NpgsqlParameter("in_userid", userId));
                command.Parameters.Add(new NpgsqlParameter("in_showtimeid", showtimeId));
                command.Parameters.Add(new NpgsqlParameter("in_seatidsjson", NpgsqlDbType.Json) { Value = seatIdsJson });

                // 4. Thực thi và đọc kết quả
                // ExecuteScalarAsync sẽ đọc giá trị của dòng đầu tiên, cột đầu tiên
                var result = await command.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    newBookingId = Convert.ToInt32(result);
                }
            }

            return newBookingId;
        }

        public async Task ConfirmPaymentAsync(int bookingId, int userId, string paymentMethod)
        {
            var p_bookingid = new NpgsqlParameter("p_bookingid", bookingId);
            var p_userid = new NpgsqlParameter("p_userid", userId);
            var p_paymentmethod = new NpgsqlParameter("p_paymentmethod", paymentMethod);

            // Function này trả về VOID, nên dùng ExecuteSqlRawAsync
            await _context.Database.ExecuteSqlRawAsync(
                "SELECT * FROM usp_confirmbookingpayment(@p_bookingid, @p_userid, @p_paymentmethod)",
                p_bookingid, p_userid, p_paymentmethod
            );
        }

        public async Task<List<BookingHistoryRawDTO>> GetBookingHistoryAsync(int userId)
        {
            var p_userid = new NpgsqlParameter("p_userid", userId);

            // Function này trả về bảng, nên dùng FromSqlRaw
            return await _context.Set<BookingHistoryRawDTO>()
                .FromSqlRaw("SELECT * FROM usp_getbookinghistory(@p_userid)", p_userid)
                .ToListAsync();
        }
    }
}
