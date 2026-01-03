using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.BUS;
using System.Security.Claims;

namespace MovieBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Yêu cầu phải có Token hợp lệ mới được truy cập
    public class UserController : ControllerBase
    {
        private readonly IUserBUS _userService;

        public UserController(IUserBUS userService)
        {
            _userService = userService;
        }

        // GET: api/user/me
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            try
            {
                // Trích xuất UserId từ Token (Claim Types.NameIdentifier)
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { Message = "Token không hợp lệ hoặc thiếu thông tin định danh." });
                }

                int userId = int.Parse(userIdClaim.Value);

                var profile = await _userService.GetUserProfileAsync(userId);

                if (profile == null)
                {
                    return NotFound(new { Message = "Không tìm thấy thông tin người dùng." });
                }

                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // PUT: api/user/me
        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Trích xuất UserId từ Token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { Message = "Token không hợp lệ." });
                }

                int userId = int.Parse(userIdClaim.Value);

                // Gọi Service
                bool isUpdated = await _userService.UpdateUserProfileAsync(userId, request);

                if (isUpdated)
                {
                    return Ok(new { Message = "Cập nhật hồ sơ thành công." });
                }
                else
                {
                    return BadRequest(new { Message = "Cập nhật thất bại. Có thể Email đã tồn tại hoặc người dùng không tồn tại." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // PUT: api/user/password
        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null) return Unauthorized();

                int userId = int.Parse(userIdClaim.Value);

                var result = await _userService.ChangePasswordAsync(userId, request);

                if (result.Success)
                {
                    return Ok(new { Message = result.Message });
                }
                else
                {
                    return BadRequest(new { Message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}