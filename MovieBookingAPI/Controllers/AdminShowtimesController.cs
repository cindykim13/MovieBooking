using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Services;
using System;
using System.Threading.Tasks;


namespace MovieBookingAPI.Controllers
{
    [Route("api/admin/showtimes")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Bỏ comment khi đã có user Admin thực tế
    public class AdminShowtimesController : ControllerBase
    {
        private readonly IAdminShowtimeService _service;


        public AdminShowtimesController(IAdminShowtimeService service)
        {
            _service = service;
        }


        // POST: api/admin/showtimes
        [HttpPost]
        public async Task<IActionResult> CreateShowtime([FromBody] CreateShowtimeRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                int newId = await _service.CreateShowtimeAsync(request);
                return StatusCode(201, new { Message = "Tạo lịch chiếu thành công.", ShowtimeId = newId });
            }
            catch (InvalidOperationException ex) // Lỗi trùng lịch (Conflict)
            {
                return Conflict(new { Message = ex.Message });
            }
            catch (ArgumentException ex) // Lỗi validation dữ liệu
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
        // PUT: api/admin/showtimes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShowtime(int id, [FromBody] UpdateShowtimeRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                await _service.UpdateShowtimeAsync(id, request);
                return Ok(new { Message = "Cập nhật lịch chiếu thành công." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex) // Lỗi nghiệp vụ (trùng lịch, đã bán vé)
            {
                return Conflict(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowtime(int id)
        {
            try
            {
                await _service.DeleteShowtimeAsync(id);
                // HTTP 204 No Content là response chuẩn cho lệnh DELETE thành công
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}
