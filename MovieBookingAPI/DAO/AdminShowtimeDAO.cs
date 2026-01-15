using Microsoft.EntityFrameworkCore;
using MovieBooking.Domain.DTOs;
using MovieBooking.Domain.Entities; // Namespace chứa class Showtime của bạn
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.Entities; // Namespace chứa ScreenRoom/Cinema (kiểm tra lại nếu khác)

namespace MovieBookingAPI.DAO
{
    public class AdminShowtimeDAO : IAdminShowtimeDAO
    {
        private readonly AppDbContext _context;

        public AdminShowtimeDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetAllShowtimesAsync()
        {
            return await _context.Showtimes
               .Include(s => s.Movie)
               .Include(s => s.Room)            // Cần dòng "virtual Room" bên Entity mới chạy được
               .ThenInclude(r => r.Cinema)
               .Select(s => new ShowtimeDTO
               {
                   // SỬA: Dùng ShowtimeId thay vì Id
                   ShowtimeId = s.ShowtimeId,

                   MovieTitle = s.Movie.Title,
                   RoomName = s.Room.RoomName,
                   CinemaName = s.Room.Cinema.Name,
                   StartTime = s.StartTime,
                   EndTime = s.EndTime,
                   Price = s.Price,
                   Status = s.StartTime > DateTime.Now ? "Upcoming" : "Ended"
               })
               .ToListAsync();
        }

        public async Task<bool> CreateShowtimeAsync(CreateShowtimeRequestDTO req)
        {
            var movie = await _context.Movies.FindAsync(req.MovieId);
            if (movie == null) return false;

            var showtime = new Showtime
            {
                MovieId = req.MovieId,
                RoomId = req.RoomId,
                StartTime = req.StartTime,
                EndTime = req.StartTime.AddMinutes(movie.Duration),
                Price = req.BasePrice,
                Status = 1
            };

            _context.Showtimes.Add(showtime);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateShowtimeAsync(int id, UpdateShowtimeRequestDTO req)
        {
            // SỬA: Find theo id truyền vào
            var showtime = await _context.Showtimes.FindAsync(id);
            if (showtime == null) return false;

            var movie = await _context.Movies.FindAsync(req.MovieId);

            showtime.MovieId = req.MovieId;
            showtime.RoomId = req.RoomId;
            showtime.StartTime = req.StartTime;
            if (movie != null)
                showtime.EndTime = req.StartTime.AddMinutes(movie.Duration);
            showtime.Price = req.BasePrice;

            _context.Showtimes.Update(showtime);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteShowtimeAsync(int id)
        {
            var item = await _context.Showtimes.FindAsync(id);
            if (item == null) return false;
            _context.Showtimes.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date)
        {
            return await _context.Showtimes
               .Include(s => s.Movie)
               .Include(s => s.Room)
               .ThenInclude(r => r.Cinema)
               .Where(s => s.StartTime.Date == date.Date)
               .Select(s => new ShowtimeDTO
               {
                   // SỬA: Dùng ShowtimeId
                   ShowtimeId = s.ShowtimeId,

                   MovieTitle = s.Movie.Title,
                   RoomName = s.Room.RoomName,
                   CinemaName = s.Room.Cinema.Name,
                   StartTime = s.StartTime,
                   EndTime = s.EndTime,
                   Price = s.Price,
                   Status = s.StartTime > DateTime.Now ? "Upcoming" : "Ended"
               })
               .ToListAsync();
        }
    }
}