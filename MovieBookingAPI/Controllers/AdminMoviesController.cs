using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.Controllers
{
    [Route("api/admin/movies")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminMoviesController : ControllerBase
    {
        private readonly IAdminMovieService _adminMovieService;


        public AdminMoviesController(IAdminMovieService adminMovieService)
        {
            _adminMovieService = adminMovieService;
        }


        // POST: api/admin/movies/import
        [HttpPost("import")]
        public async Task<IActionResult> ImportMovies(IFormFile file)
        {
            try
            {
                int count = await _adminMovieService.ImportMoviesFromFileAsync(file);
                return Ok(new { Message = $"Nhập liệu thành công. Đã thêm {count} phim mới." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
        // POST: api/admin/movies
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] AddMovieRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                int newMovieId = await _adminMovieService.AddMovieAsync(request);

                // Trả về 201 Created và kèm theo Location Header trỏ đến API xem chi tiết phim
                return CreatedAtAction(
                    actionName: "GetMovieDetail", // Tên Action trong MoviesController
                    controllerName: "Movies",     // Tên Controller chứa Action đó
                    routeValues: new { id = newMovieId },
                    value: new { Message = "Thêm phim thành công.", MovieId = newMovieId }
                );
            }
            catch (ArgumentException ex)
            {
                // Lỗi trùng lặp phim
                return Conflict(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi hệ thống: " + ex.Message });
            }
        }
        // PUT: api/admin/movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateMovieRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                await _adminMovieService.UpdateMovieAsync(id, request);
                return Ok(new { Message = "Cập nhật thông tin phim thành công." });
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

        // DELETE: api/admin/movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _adminMovieService.DeleteMovieAsync(id);
                return Ok(new { Message = "Xóa phim thành công." });
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
