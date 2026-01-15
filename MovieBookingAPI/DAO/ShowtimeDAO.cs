using Microsoft.EntityFrameworkCore;
using MovieBooking.Domain.DTOs;
using MovieBooking.Domain.Entities; // Nhớ using Entity
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public class ShowtimeDAO : IShowtimeDAO
    {
        private readonly AppDbContext _context;

        public ShowtimeDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date)
        {
            // Logic tìm từ 00:00 hôm nay đến 00:00 hôm sau
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            var query = from s in _context.Showtimes
                        join m in _context.Movies on s.MovieId equals m.MovieId into mGroup
                        from m in mGroup.DefaultIfEmpty()

                        join r in _context.ScreenRooms on s.RoomId equals r.Id into rGroup
                        from r in rGroup.DefaultIfEmpty()

                        join c in _context.Cinemas on (r == null ? 0 : r.CinemaId) equals c.CinemaId into cGroup
                        from c in cGroup.DefaultIfEmpty()

                            // Lọc theo ngày
                        where s.StartTime >= startOfDay && s.StartTime < endOfDay
                        orderby s.StartTime

                        // [QUAN TRỌNG] Phải map đúng tên biến trong ShowtimeDTO
                        select new ShowtimeDTO
                        {
                            ShowtimeId = s.ShowtimeId,

                            // Lấy tên để hiển thị
                            MovieTitle = m != null ? m.Title : "Unknown Movie",
                            RoomName = r != null ? r.RoomName : "Unknown Room",
                            CinemaName = c != null ? c.Name : "Unknown Cinema",

                            StartTime = s.StartTime,
                            EndTime = s.EndTime,
                            Price = s.Price,
                            Status = s.Status == 1 ? "Active" : "Stopped"
                        };

            return await query.ToListAsync();
        }
        // ... (Giữ nguyên các hàm Create, Update, Delete ở dưới) ...
        public async Task<int> CreateShowtimeAsync(int movieId, int roomId, DateTime startTime, decimal price)
        {
            var showtime = new Showtime
            {
                MovieId = movieId,
                RoomId = roomId,
                StartTime = startTime,
                Price = price,
                Status = 1
            };
            _context.Showtimes.Add(showtime);
            await _context.SaveChangesAsync();
            return showtime.ShowtimeId;
        }

        public async Task DeleteShowtimeAsync(int id)
        {
            var showtime = await _context.Showtimes.FindAsync(id);
            if (showtime != null)
            {
                _context.Showtimes.Remove(showtime);
                await _context.SaveChangesAsync();
            }
        }

        // (Các hàm khác giữ nguyên...)
        public async Task UpdateShowtimeAsync(int id, int movieId, int roomId, DateTime startTime, decimal price)
        {
            var showtime = await _context.Showtimes.FindAsync(id);
            if (showtime != null)
            {
                showtime.MovieId = movieId;
                showtime.RoomId = roomId;
                showtime.StartTime = startTime;
                showtime.Price = price;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Showtime?> GetShowtimeByIdAsync(int id)
        {
            return await _context.Showtimes.FindAsync(id);
        }

        public async Task<IEnumerable<Showtime>> GetShowtimesByMovieAsync(int movieId, DateTime date)
        {
            return await _context.Showtimes.Where(s => s.MovieId == movieId).ToListAsync();
        }

        public async Task<Showtime?> GetShowtimeDetailAsync(int showtimeId)
        {
            return await _context.Showtimes.FindAsync(showtimeId);
        }
        public async Task<IEnumerable<Seat>> GetSeatMapAsync(int showtimeId)
        {
            // 1. Lấy thông tin lịch chiếu để biết RoomId
            var showtime = await _context.Showtimes.FindAsync(showtimeId);
            if (showtime == null) return new List<Seat>();

            // 2. Lấy danh sách ghế dựa theo RoomId
            var seats = await _context.Seats
                .Where(s => s.RoomId == showtime.RoomId)
                .OrderBy(s => s.SeatRow)
                .ThenBy(s => s.SeatNumber)
                .ToListAsync();

            return seats;
        }
    }
}