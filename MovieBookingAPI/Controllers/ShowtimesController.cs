using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.BUS;
using Microsoft.AspNetCore.Authorization;

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

        // GET: api/showtimes?movieId=1&date=2025-12-20
        [HttpGet]
        public async Task<IActionResult> GetShowtimes(
            [FromQuery] int movieId,
            [FromQuery] DateTime? date)
        {
            if (movieId <= 0)
            {
                return BadRequest(new { Message = "MovieId không hợp lệ." });
            }

            try
            {
                var result = await _showtimeService.GetShowtimesByMovieAsync(movieId, date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // GET: api/showtimes/5/seats
        [HttpGet("{id}/seats")]
        [Authorize] // Bảo vệ Endpoint
        public async Task<IActionResult> GetSeatMap(int id)
        {
            try
            {
                var seats = await _showtimeService.GetSeatMapAsync(id);

                // Nếu danh sách rỗng, có thể là do ShowtimeId sai hoặc chưa tạo ghế
                if (seats == null || seats.Count == 0)
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