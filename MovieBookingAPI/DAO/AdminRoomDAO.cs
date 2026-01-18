using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.DTOs;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
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
                new NpgsqlParameter("p_cinemaid", request.CinemaId),
                new NpgsqlParameter("p_name", request.Name),
                // Chỉ định rõ kiểu JSON cho tham số JSON
                new NpgsqlParameter("p_seatsjson", NpgsqlDbType.Json) { Value = seatsJson }
            };

            // Sử dụng cú pháp gọi Function của PostgreSQL: SELECT func(...)
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "SELECT usp_createroomwithseats(@p_cinemaid, @p_name, @p_seatsjson)",
                    parameters
                )
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task DeleteRoomAsync(int roomId)
        {
            var parameter = new NpgsqlParameter("p_roomid", roomId);

            // Function usp_deletescreenroom trả về VOID, dùng SELECT để gọi
            await _context.Database.ExecuteSqlRawAsync(
                "SELECT usp_deletescreenroom(@p_roomid)",
                parameter
            );
        }
        public async Task<List<RoomTemplateDTO>> GetAllTemplatesAsync()
        {
            var templates = await _context.RoomTemplates.ToListAsync();

            return templates.Select(t => new RoomTemplateDTO
            {
                TemplateId = t.TemplateId,
                TemplateName = t.TemplateName,
                TotalSeats = t.TotalSeats,
                // Deserialize JSON từ DB thành List Object
                Seats = JsonConvert.DeserializeObject<List<SeatDefinitionDTO>>(t.LayoutJson)
                        ?? new List<SeatDefinitionDTO>()
            }).ToList();
        }
    }
}
