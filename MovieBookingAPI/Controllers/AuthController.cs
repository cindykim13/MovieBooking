using Microsoft.AspNetCore.Mvc;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.BUS;

namespace MovieBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBUS _authService;

        public AuthController(IAuthBUS authService)
        {
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
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
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
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

        // THÊM PHƯƠNG THỨC NÀY VÀO
        // GET: api/auth/hash-password?password=your_password
        [HttpGet("hash-password")]
        public IActionResult HashPassword([FromQuery] string password)
        {
            // CẢNH BÁO BẢO MẬT: 
            // API NÀY CHỈ DÙNG CHO MÔI TRƯỜNG DEVELOPMENT ĐỂ TẠO HASH.
            // TUYỆT ĐỐI KHÔNG TRIỂN KHAI LÊN PRODUCTION.
            if (string.IsNullOrEmpty(password))
            {
                return BadRequest("Vui lòng cung cấp mật khẩu qua query parameter 'password'.");
            }

            // Sử dụng thư viện BCrypt để tạo hash
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // Trả về kết quả để bạn có thể copy
            return Ok(new
            {
                PlainPassword = password,
                HashedPassword = passwordHash
            });
        }
    }
}