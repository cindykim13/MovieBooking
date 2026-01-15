using Microsoft.AspNetCore.Http;
using MovieBookingAPI.DAO;
using MovieBooking.Domain.DTOs;
using Newtonsoft.Json;
using Npgsql; // Sử dụng Npgsql cho Postgres
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;

namespace MovieBookingAPI.BUS
{
    public class AdminMovieBUS : IAdminMovieBUS
    {
        private readonly IAdminMovieDAO _adminMovieDAO;

        public AdminMovieBUS(IAdminMovieDAO adminMovieDAO)
        {
            _adminMovieDAO = adminMovieDAO;
        }

        public async Task<int> ImportMoviesFromFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File không hợp lệ hoặc rỗng.");
            }

            // 1. Đọc nội dung file
            string fileContent;
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                fileContent = await stream.ReadToEndAsync();
            }

            // 2. Validate định dạng JSON
            List<MovieImportDTO> movies;
            try
            {
                movies = JsonConvert.DeserializeObject<List<MovieImportDTO>>(fileContent);
                if (movies == null || movies.Count == 0)
                {
                    throw new ArgumentException("File không chứa dữ liệu phim hợp lệ.");
                }
            }
            catch (JsonException)
            {
                throw new ArgumentException("Định dạng file không phải là JSON hợp lệ.");
            }

            // 3. Serialize lại thành chuỗi JSON chuẩn để gửi xuống DB
            string jsonForDb = JsonConvert.SerializeObject(movies);

            // 4. Gọi DAO và xử lý Exception
            try
            {
                return await _adminMovieDAO.ImportMoviesAsync(jsonForDb);
            }
            catch (PostgresException ex)
            {
                throw new Exception("Lỗi cơ sở dữ liệu khi nhập liệu phim: " + ex.Message, ex);
            }
        }

        public async Task<int> AddMovieAsync(AddMovieRequestDTO request)
        {
            try
            {
                return await _adminMovieDAO.AddMovieAsync(request);
            }
            catch (PostgresException ex)
            {
                // Bắt lỗi nghiệp vụ từ Function (dựa vào MessageText trả về từ RAISE EXCEPTION trong Postgres)
                if (ex.MessageText.Contains("Phim này đã tồn tại") || ex.MessageText.Contains("already exists"))
                {
                    throw new ArgumentException("Phim này đã tồn tại trong hệ thống (Trùng tên và năm phát hành).");
                }
                throw new Exception("Lỗi cơ sở dữ liệu khi thêm phim: " + ex.Message, ex);
            }
        }

        public async Task UpdateMovieAsync(int movieId, UpdateMovieRequestDTO request)
        {
            try
            {
                await _adminMovieDAO.UpdateMovieAsync(movieId, request);
            }
            catch (PostgresException ex)
            {
                // Bắt lỗi khi ID không tồn tại
                if (ex.MessageText.Contains("Phim không tồn tại") || ex.MessageText.Contains("not found"))
                {
                    throw new KeyNotFoundException($"Không tìm thấy phim với ID = {movieId}");
                }
                throw new Exception("Lỗi cơ sở dữ liệu khi cập nhật phim: " + ex.Message, ex);
            }
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            try
            {
                await _adminMovieDAO.DeleteMovieAsync(movieId);
            }
            catch (PostgresException ex)
            {
                // --- XỬ LÝ LỖI POSTGRES ---

                // Trường hợp 1: Vi phạm khóa ngoại (Foreign Key Violation)
                // Ví dụ: Phim đã có trong bảng Showtimes hoặc Bookings
                // Mã lỗi chuẩn của Postgres cho FK violation là "23503"
                if (ex.SqlState == "23503" || ex.MessageText.Contains("đã có lịch chiếu"))
                {
                    throw new InvalidOperationException("Không thể xóa phim này vì đã có lịch chiếu hoặc dữ liệu liên quan.");
                }

                // Trường hợp 2: Lỗi custom từ Stored Procedure (RAISE EXCEPTION)
                // Ví dụ: SP kiểm tra ID không thấy và RAISE 'Phim không tồn tại'
                if (ex.MessageText.Contains("Phim không tồn tại") || ex.MessageText.Contains("not found"))
                {
                    throw new KeyNotFoundException($"Không tìm thấy phim với ID = {movieId}");
                }

                throw new Exception("Lỗi cơ sở dữ liệu khi xóa phim: " + ex.Message, ex);
            }
        }
    }
}