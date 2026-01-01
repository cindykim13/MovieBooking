using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.BUS;
using System;
using System.Threading.Tasks;


namespace MovieBookingAPI.Controllers
{
    [Route("api/admin/rooms")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminRoomsController : ControllerBase
    {
        private readonly IAdminRoomBUS _service;


        public AdminRoomsController(IAdminRoomBUS service)
        {
            _service = service;
        }


        // POST: api/admin/rooms
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                int newId = await _service.CreateRoomAsync(request);
                return StatusCode(201, new { Message = "Tạo phòng chiếu thành công.", RoomId = newId });
            }
            catch (ArgumentException ex)
            {
                // Lỗi nghiệp vụ (trùng tên, ghế trống...)
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
        // DELETE: api/admin/rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                await _service.DeleteRoomAsync(id);
                // HTTP 204 No Content là response chuẩn cho lệnh DELETE thành công
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Trả về 409 Conflict khi vi phạm ràng buộc nghiệp vụ
                return Conflict(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}
