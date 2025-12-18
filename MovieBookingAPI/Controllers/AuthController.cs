using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Services;

namespace MovieBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            // Kiểm tra Validation của DTO (ModelState)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _authService.RegisterAsync(request);

                if (result.Success)
                {
                    // Trả về 201 Created cùng ID người dùng mới
                    return StatusCode(201, new
                    {
                        Message = result.Message,
                        UserId = result.UserId
                    });
                }
                else
                {
                    // Trả về 409 Conflict (cho lỗi trùng lặp) hoặc 400 BadRequest
                    return Conflict(new { Message = result.Message });
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi tại đây nếu cần
                return StatusCode(500, new { Message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }
        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _authService.LoginAsync(request);

                if (response == null)
                {
                    // Trả về 401 nếu sai tên đăng nhập hoặc mật khẩu
                    return Unauthorized(new { Message = "Tên đăng nhập hoặc mật khẩu không chính xác." });
                }

                // Trả về 200 OK kèm Token
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}