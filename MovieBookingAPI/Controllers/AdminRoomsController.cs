using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.BUS;

namespace MovieBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Admin")] // Bỏ comment nếu muốn bật bảo mật
    public class AdminRoomsController : ControllerBase
    {
        private readonly IAdminRoomBUS _adminRoomBUS;

        public AdminRoomsController(IAdminRoomBUS adminRoomBUS)
        {
            _adminRoomBUS = adminRoomBUS;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            // API này vẫn cần thiết để ComboBox chọn phòng hiển thị danh sách
            var result = await _adminRoomBUS.GetAllRoomsAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminRoomBUS.CreateRoomAsync(dto);
            if (result)
                return Ok(new { Message = "Tạo phòng và ghế thành công" });

            return BadRequest(new { Message = "Tạo phòng thất bại" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _adminRoomBUS.DeleteRoomAsync(id);
            if (result)
                return Ok(new { Message = "Xóa thành công" });

            return BadRequest(new { Message = "Xóa thất bại (ID không tồn tại)" });
        }
    }
}