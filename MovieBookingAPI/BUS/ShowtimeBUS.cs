using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.DAO;

namespace MovieBookingAPI.BUS
{
    public class ShowtimeBUS : IShowtimeBUS
    {
        private readonly IShowtimeDAO _showtimeRepo;

        public ShowtimeBUS(IShowtimeDAO showtimeRepo)
        {
            _showtimeRepo = showtimeRepo;
        }

        public async Task<List<CinemaShowtimeDTO>> GetShowtimesByMovieAsync(int movieId, DateTime? date)
        {
            // Nếu không truyền ngày, mặc định lấy ngày hiện tại
            DateTime filterDate = date ?? DateTime.Now.Date;

            // 1. Lấy dữ liệu thô từ Repository
            var rawData = await _showtimeRepo.GetRawShowtimesAsync(movieId, filterDate);

            // 2. Thực hiện Grouping (Logic quan trọng)
            // Gom nhóm theo CinemaId để tạo cấu trúc phân cấp
            var result = rawData
                .GroupBy(x => new { x.CinemaId, x.CinemaName, x.CinemaAddress })
                .Select(g => new CinemaShowtimeDTO
                {
                    CinemaId = g.Key.CinemaId,
                    CinemaName = g.Key.CinemaName,
                    Address = g.Key.CinemaAddress,
                    Showtimes = g.Select(s => new ShowtimeDTO
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

        public async Task<List<SeatDTO>> GetSeatMapAsync(int showtimeId)
        {
            // Có thể thêm logic kiểm tra showtimeId > 0
            return await _showtimeRepo.GetSeatMapAsync(showtimeId);
        }
    }
}