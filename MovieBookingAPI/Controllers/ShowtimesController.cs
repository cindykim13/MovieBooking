using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.BUS;
using System;
using System.Threading.Tasks;
using System.Linq; // Để dùng .Count()

namespace MovieBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimesController : ControllerBase
    {
        private readonly IShowtimeBUS _showtimeService;

        public ShowtimesController(IShowtimeBUS showtimeService)
        {
            _showtimeService = showtimeService;
        }

        // GET: api/showtimes?movieId=...&date=...
        [HttpGet]
        public async Task<IActionResult> GetShowtimes(
            [FromQuery] int? movieId,   // Đã sửa thành int? (có thể null)
            [FromQuery] DateTime? date) // Đã sửa thành DateTime? (có thể null)
        {
            try
            {
                // TRƯỜNG HỢP 1: Admin xem lịch chiếu theo ngày (movieId = null hoặc 0)
                if ((movieId == null || movieId <= 0) && date.HasValue)
                {
                    // Gọi hàm tìm theo ngày (Admin)
                    var result = await _showtimeService.GetShowtimesByDateAsync(date.Value);

                    // Trả về danh sách rỗng thay vì 404 để Client không báo lỗi
                    if (result == null) return Ok(new List<object>());

                    return Ok(result);
                }

                // TRƯỜNG HỢP 2: Khách hàng xem lịch chiếu của 1 phim cụ thể
                if (movieId.HasValue && movieId > 0)
                {
                    // Gọi hàm tìm theo phim (Client)
                    var result = await _showtimeService.GetShowtimesByMovieAsync(movieId.Value, date ?? DateTime.Now);
                    return Ok(result);
                }

                return BadRequest(new { Message = "Vui lòng cung cấp MovieId (để tìm theo phim) hoặc Date (để tìm theo ngày)." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // GET: api/showtimes/5/seats
        [HttpGet("{id}/seats")]
        public async Task<IActionResult> GetSeatMap(int id)
        {
            try
            {
                var seats = await _showtimeService.GetSeatMapAsync(id);

                if (seats == null || seats.Count() == 0)
                {
                    return NotFound(new { Message = "Không tìm thấy sơ đồ ghế cho suất chiếu này." });
                }

                return Ok(seats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}