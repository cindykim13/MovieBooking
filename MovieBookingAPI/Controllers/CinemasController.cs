using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.BUS;
using System;
using System.Threading.Tasks;

namespace MovieBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly ICinemaBUS _cinemaBUS;

        public CinemasController(ICinemaBUS cinemaBUS)
        {
            _cinemaBUS = cinemaBUS;
        }

        // GET: api/cinemas
        [HttpGet]
        public async Task<IActionResult> GetAllCinemas()
        {
            try
            {
                var cinemas = await _cinemaBUS.GetAllCinemasAsync();
                return Ok(cinemas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
        // GET: api/cinemas/rooms?cinemaId=1 (Lấy theo rạp)
        // GET: api/cinemas/rooms (Lấy tất cả)
        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms([FromQuery] int? cinemaId)
        {
            try
            {
                var rooms = await _cinemaBUS.GetRoomsAsync(cinemaId);
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}