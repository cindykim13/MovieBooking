using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Services;
using System.Security.Claims;

namespace MovieBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Bắt buộc đăng nhập
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Lấy UserId từ Token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null) return Unauthorized();

                int userId = int.Parse(userIdClaim.Value);

                var result = await _bookingService.CreateBookingAsync(userId, request);

                if (result.Success)
                {
                    // Trả về 201 Created và thông tin BookingId để chuyển sang thanh toán
                    return StatusCode(201, new
                    {
                        Message = result.Message,
                        BookingId = result.BookingId
                    });
                }
                else
                {
                    // Trả về 409 Conflict cho lỗi nghiệp vụ (trùng ghế)
                    return Conflict(new { Message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // POST: api/bookings/payment
        [HttpPost("payment")]
        public async Task<IActionResult> ConfirmPayment([FromBody] PaymentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Lấy UserId từ Token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null) return Unauthorized();

                int userId = int.Parse(userIdClaim.Value);

                var result = await _bookingService.ProcessPaymentAsync(userId, request);

                if (result.Success)
                {
                    return Ok(new { Message = result.Message });
                }
                else
                {
                    // Trả về 400 Bad Request cho các lỗi nghiệp vụ (Hết hạn, Sai user...)
                    return BadRequest(new { Message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // GET: api/bookings/history
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            try
            {
                // Lấy UserId từ Token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null) return Unauthorized();

                int userId = int.Parse(userIdClaim.Value);

                var history = await _bookingService.GetBookingHistoryAsync(userId);

                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}