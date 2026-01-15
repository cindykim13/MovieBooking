using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.BUS;
using MovieBooking.Domain.DTOs; // Nhớ using DTO
using System;
using System.Threading.Tasks;

namespace MovieBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminShowtimesController : ControllerBase
    {
        private readonly IShowtimeBUS _showtimeBUS;

        public AdminShowtimesController(IShowtimeBUS showtimeBUS)
        {
            _showtimeBUS = showtimeBUS;
        }

        // 1. THÊM LỊCH CHIẾU
        [HttpPost]
        public async Task<IActionResult> CreateShowtime([FromBody] CreateShowtimeRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // [SỬA LỖI CS7036]: Truyền từng tham số lẻ thay vì truyền cả cục 'request'
                var newId = await _showtimeBUS.AddShowtimeAsync(
                    request.MovieId,
                    request.RoomId,
                    request.StartTime,
                    request.Price
                );

                return Ok(newId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi thêm lịch chiếu: " + ex.Message);
            }
        }

        // 2. SỬA LỊCH CHIẾU
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShowtime(int id, [FromBody] UpdateShowtimeRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // [SỬA LỖI CS7036]: Truyền từng tham số lẻ
                await _showtimeBUS.UpdateShowtimeAsync(
                    id,
                    request.MovieId,
                    request.RoomId,
                    request.StartTime,
                    request.BasePrice // Lưu ý: Trong DTO bạn đặt là BasePrice hay Price thì dùng cái đó
                );

                return Ok("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi cập nhật: " + ex.Message);
            }
        }

        // 3. XÓA LỊCH CHIẾU
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowtime(int id)
        {
            try
            {
                await _showtimeBUS.DeleteShowtimeAsync(id);
                return Ok("Xóa thành công!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi xóa: " + ex.Message);
            }
        }
    }
}