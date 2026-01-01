using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.DTOs;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;


namespace MovieBookingAPI.DAO
{
    public class AdminRoomDAO : IAdminRoomDAO
    {
        private readonly AppDbContext _context;


        public AdminRoomDAO(AppDbContext context)
        {
            _context = context;
        }


        public async Task<int> CreateRoomWithSeatsAsync(CreateRoomRequestDTO request)
        {
            // Serialize danh sách ghế thành chuỗi JSON
            string seatsJson = JsonConvert.SerializeObject(request.Seats);


            var parameters = new[]
            {
                new SqlParameter("@CinemaId", request.CinemaId),
                new SqlParameter("@Name", request.Name),
                new SqlParameter("@SeatsJson", seatsJson)
            };


            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "EXEC usp_CreateRoomWithSeats @CinemaId, @Name, @SeatsJson",
                    parameters
                )
                .ToListAsync();


            return result.FirstOrDefault();
        }
        public async Task DeleteRoomAsync(int roomId)
        {
            var parameter = new SqlParameter("@RoomId", roomId);


            await _context.Database.ExecuteSqlRawAsync(
                "EXEC usp_DeleteScreenRoom @RoomId",
                parameter
            );
        }

    }
}
