using Microsoft.EntityFrameworkCore;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Data;
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
    }
}