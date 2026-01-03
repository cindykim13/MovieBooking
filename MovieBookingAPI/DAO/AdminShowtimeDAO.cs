using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.DAO;
using MovieBookingAPI.Data;
using MovieBooking.Domain.DTOs;
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


        public async Task<int> CreateShowtimeAsync(CreateShowtimeRequestDTO request)
        {
            var parameters = new[]
            {
                new SqlParameter("@MovieId", request.MovieId),
                new SqlParameter("@RoomId", request.RoomId),
                new SqlParameter("@StartTime", request.StartTime),
                new SqlParameter("@BasePrice", request.BasePrice),
                new SqlParameter("@CleaningTimeMinutes", 15) // Giá trị mặc định hoặc lấy từ config
            };


            // Sử dụng SqlQueryRaw để lấy giá trị vô hướng (NewShowtimeId)
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "EXEC usp_CreateShowtime @MovieId, @RoomId, @StartTime, @BasePrice, @CleaningTimeMinutes",
                    parameters
                )
                .ToListAsync();


            return result.FirstOrDefault();
        }
        public async Task UpdateShowtimeAsync(int showtimeId, UpdateShowtimeRequestDTO request)
        {
            var parameters = new[]
            {
        new SqlParameter("@ShowtimeId", showtimeId),
        new SqlParameter("@MovieId", request.MovieId),
        new SqlParameter("@RoomId", request.RoomId),
        new SqlParameter("@StartTime", request.StartTime),
        new SqlParameter("@BasePrice", request.BasePrice),
        new SqlParameter("@Status", request.Status)
    };


            await _context.Database.ExecuteSqlRawAsync(
                "EXEC usp_UpdateShowtime @ShowtimeId, @MovieId, @RoomId, @StartTime, @BasePrice, @Status",
                parameters
            );
        }
        public async Task DeleteShowtimeAsync(int showtimeId)
        {
            var parameter = new SqlParameter("@ShowtimeId", showtimeId);


            await _context.Database.ExecuteSqlRawAsync(
                "EXEC usp_DeleteShowtime @ShowtimeId",
                parameter
            );
        }

    }
}
