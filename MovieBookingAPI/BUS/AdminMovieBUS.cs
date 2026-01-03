using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using MovieBookingAPI.DAO;
using MovieBooking.Domain.DTOs;
using Newtonsoft.Json;
using Npgsql;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
            // Thử deserialize để đảm bảo file đúng cấu trúc DTO
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


            // 3. Serialize lại thành chuỗi JSON chuẩn
            string jsonForDb = JsonConvert.SerializeObject(movies);

            // 4. Gọi DAO và xử lý Exception
            try
            {
                return await _adminMovieDAO.ImportMoviesAsync(jsonForDb);
            }
            catch (PostgresException ex) // Bắt đúng loại Exception của Npgsql
            {
                // Có thể thêm logic log lỗi chi tiết tại đây
                throw new Exception("Lỗi cơ sở dữ liệu khi nhập liệu phim: " + ex.Message, ex);
            }
        }
        public async Task<int> AddMovieAsync(AddMovieRequestDTO request)
        {
            try
            {
                return await _adminMovieDAO.AddMovieAsync(request);
            }
            catch (PostgresException ex) // Sửa thành PostgresException
            {
                // Bắt lỗi nghiệp vụ từ Function
                if (ex.Message.Contains("Phim này đã tồn tại"))
                {
                    throw new ArgumentException("Phim này đã tồn tại trong hệ thống (Trùng tên và năm phát hành).");
                }
                throw new Exception("Lỗi cơ sở dữ liệu khi thêm phim.", ex);
            }
        }
        public async Task UpdateMovieAsync(int movieId, UpdateMovieRequestDTO request)
        {
            try
            {
                await _adminMovieDAO.UpdateMovieAsync(movieId, request);
            }
            catch (PostgresException ex) // Sửa thành PostgresException
            {
                // Bắt lỗi nghiệp vụ từ Function
                if (ex.Message.Contains("Phim không tồn tại"))
                {
                    throw new KeyNotFoundException($"Không tìm thấy phim với ID = {movieId}");
                }
                throw new Exception("Lỗi cơ sở dữ liệu khi cập nhật phim.", ex);
            }
        }
        public async Task DeleteMovieAsync(int movieId)
        {
            try
            {
                await _adminMovieDAO.DeleteMovieAsync(movieId);
            }
            catch (SqlException ex)
            {
                // Lỗi 53002: Không tìm thấy
                if (ex.Number == 53002)
                {
                    throw new KeyNotFoundException($"Không tìm thấy phim với ID = {movieId}");
                }
                // Lỗi 53003: Vi phạm ràng buộc nghiệp vụ (Đã có lịch chiếu)
                if (ex.Number == 53003)
                {
                    throw new InvalidOperationException("Không thể xóa phim này vì đã có lịch chiếu liên quan.");
                }

                throw; // Lỗi khác
            }
        }

    }
}