using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.DAO;
using MovieBookingAPI.Data;
using Npgsql;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace MovieBookingAPI.DAO
{
    public class AdminShowtimeDAO : IAdminShowtimeDAO
    {
        private readonly AppDbContext _context;


        public AdminShowtimeDAO(AppDbContext context)
        {
            _context = context;
        }


        // Helper method: Kiểm tra Room có thuộc Cinema không
        public async Task<bool> IsRoomInCinemaAsync(int roomId, int cinemaId)
        {
            // Truy vấn trực tiếp bảng screenroom
            var room = await _context.Database
                .SqlQueryRaw<int>(
                    "SELECT 1 FROM screenroom WHERE roomid = {0} AND cinemaid = {1}",
                    roomId, cinemaId)
                .ToListAsync();

            return room.Any();
        }

        public async Task<int> CreateShowtimeAsync(CreateShowtimeRequestDTO request)
        {
            var parameters = new[]
            {
                new NpgsqlParameter("p_movieid", request.MovieId),
                new NpgsqlParameter("p_roomid", request.RoomId),
                // [FIX]: Chỉ định rõ kiểu Timestamp (Without Time Zone)
                new NpgsqlParameter("p_starttime", NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = request.StartTime },
                new NpgsqlParameter("p_baseprice", request.BasePrice),
                new NpgsqlParameter("p_cleaningtimeminutes", 15)
            };

            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "SELECT usp_createshowtime(@p_movieid, @p_roomid, @p_starttime, @p_baseprice, @p_cleaningtimeminutes)",
                    parameters
                )
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task UpdateShowtimeAsync(int showtimeId, UpdateShowtimeRequestDTO request)
        {
            var parameters = new[]
            {
                new NpgsqlParameter("p_showtimeid", showtimeId),
                new NpgsqlParameter("p_movieid", request.MovieId),
                new NpgsqlParameter("p_roomid", request.RoomId),
                new NpgsqlParameter("p_starttime", NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = request.StartTime },
                new NpgsqlParameter("p_baseprice", request.BasePrice),
                new NpgsqlParameter("p_status", (short)request.Status), // PostgreSQL smallint map với short
                new NpgsqlParameter("p_cleaningtimeminutes", 15)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "SELECT usp_updateshowtime(@p_showtimeid, @p_movieid, @p_roomid, @p_starttime, @p_baseprice, @p_status, @p_cleaningtimeminutes)",
                parameters
            );
        }

        // [CẬP NHẬT]: Đổi kiểu trả về thành Task<string> để hứng kết quả từ Function
        public async Task<string> DeleteShowtimeAsync(int showtimeId)
        {
            var p_showtimeid = new NpgsqlParameter("p_showtimeid", showtimeId);

            // Sử dụng SqlQueryRaw<string> để lấy giá trị TEXT trả về từ Function PostgreSQL
            // Cú pháp: SELECT usp_deleteshowtime(...)
            var result = await _context.Database
                .SqlQueryRaw<string>("SELECT usp_deleteshowtime(@p_showtimeid)", p_showtimeid)
                .ToListAsync();

            return result.FirstOrDefault() ?? string.Empty;
        }
    }
}
