using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.BUS;

namespace MovieBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieBUS _movieBUS;

        public MoviesController(IMovieBUS movieBUS)
        {
            _movieBUS = movieBUS;
        }

        // GET: api/movies?pageIndex=1&pageSize=10&sortBy=ReleaseYear
        [HttpGet]
        public async Task<IActionResult> GetMovies(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sortBy = "ReleaseYear")
        {
            try
            {
                var result = await _movieBUS.GetMoviesAsync(pageIndex, pageSize, sortBy);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // GET: api/movies/search?keyword=...&status=...&genreId=...
        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies(
            [FromQuery] string? keyword,
            [FromQuery] string? status, // Thêm tham số này vào chữ ký Action
            [FromQuery] int? genreId,
            [FromQuery] int? year,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10) // Sửa pageSize mặc định thành 10
        {
            try
            {
                // Truyền tham số 'status' vào Service
                var result = await _movieBUS.SearchMoviesAsync(keyword, status, genreId, year, pageIndex, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // GET: api/movies/genres
        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            try
            {
                var result = await _movieBUS.GetAllGenresAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        // GET: api/movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieDetail(int id)
        {
            try
            {
                var movie = await _movieBUS.GetMovieDetailAsync(id);

                if (movie == null)
                {
                    return NotFound(new { Message = $"Không tìm thấy phim với ID = {id}" });
                }

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}