using Microsoft.EntityFrameworkCore;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Data;
using MovieBookingAPI.Domain.DTOs;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public class CinemaDAO : ICinemaDAO
    {
        private readonly AppDbContext _context;

        public CinemaDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CinemaDTO>> GetAllCinemasAsync()
        {
            // Do SP trả về các cột khớp với DTO (sau khi bỏ qua case), 
            // ta có thể dùng FromSqlRaw để map trực tiếp
            return await _context.Set<CinemaDTO>()
                .FromSqlRaw("SELECT * FROM usp_getallcinemas()")
                .ToListAsync();
        }

        public async Task<List<RoomDTO>> GetRoomsByCinemaAsync(int cinemaId)
        {
            var p_cinemaid = new NpgsqlParameter("p_cinemaid", cinemaId);

            var rawResult = await _context.Set<ScreenRoomRawResult>()
                .FromSqlRaw("SELECT * FROM usp_getroomsbycinema(@p_cinemaid)", p_cinemaid)
                .ToListAsync();

            return rawResult.Select(r => new RoomDTO
            {
                RoomId = r.roomid,
                Name = r.roomname,
                TotalSeats = r.totalseats
            }).ToList();
        }
    }
}