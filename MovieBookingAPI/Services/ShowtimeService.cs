using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Repositories;

namespace MovieBookingAPI.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimeRepository _showtimeRepo;

        public ShowtimeService(IShowtimeRepository showtimeRepo)
        {
            _showtimeRepo = showtimeRepo;
        }

        public async Task<List<CinemaShowtimeDto>> GetShowtimesByMovieAsync(int movieId, DateTime? date)
        {
            // Nếu không truyền ngày, mặc định lấy ngày hiện tại
            DateTime filterDate = date ?? DateTime.Now.Date;

            // 1. Lấy dữ liệu thô từ Repository
            var rawData = await _showtimeRepo.GetRawShowtimesAsync(movieId, filterDate);

            // 2. Thực hiện Grouping (Logic quan trọng)
            // Gom nhóm theo CinemaId để tạo cấu trúc phân cấp
            var result = rawData
                .GroupBy(x => new { x.CinemaId, x.CinemaName, x.CinemaAddress })
                .Select(g => new CinemaShowtimeDto
                {
                    CinemaId = g.Key.CinemaId,
                    CinemaName = g.Key.CinemaName,
                    Address = g.Key.CinemaAddress,
                    Showtimes = g.Select(s => new ShowtimeDto
                    {
                        ShowtimeId = s.ShowtimeId,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                        RoomId = s.RoomId,
                        RoomName = s.RoomName,
                        Price = s.BasePrice
                    }).ToList()
                })
                .ToList();

            return result;
        }

        public async Task<List<SeatDto>> GetSeatMapAsync(int showtimeId)
        {
            // Có thể thêm logic kiểm tra showtimeId > 0
            return await _showtimeRepo.GetSeatMapAsync(showtimeId);
        }
    }
}