using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.DAO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace MovieBookingAPI.BUS
{
    public class AdminMovieBUS : IAdminMovieBUS
    {
        private readonly IAdminMovieDAO _adminMovieRepo;


        public AdminMovieBUS(IAdminMovieDAO adminMovieRepo)
        {
            _adminMovieRepo = adminMovieRepo;
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


            // 3. Serialize lại thành chuỗi JSON chuẩn để gửi xuống SQL
            // Việc này giúp loại bỏ các khoảng trắng thừa hoặc lỗi định dạng tiềm ẩn trong file gốc
            string jsonForDb = JsonConvert.SerializeObject(movies);


            // 4. Gọi Repository
            return await _adminMovieRepo.ImportMoviesAsync(jsonForDb);
        }
        public async Task<int> AddMovieAsync(AddMovieRequestDTO request)
        {
            // Có thể thêm logic nghiệp vụ tại đây (VD: Kiểm tra URL ảnh hợp lệ, chuẩn hóa tên...)

            try
            {
                return await _adminMovieRepo.AddMovieAsync(request);
            }
            catch (SqlException ex)
            {
                // Bắt lỗi nghiệp vụ từ SP (Lỗi trùng lặp 53001)
                if (ex.Number == 53001)
                {
                    throw new ArgumentException("Phim này đã tồn tại trong hệ thống (Trùng tên và năm phát hành).");
                }
                throw; // Lỗi khác ném tiếp cho Controller
            }
        }
        public async Task UpdateMovieAsync(int movieId, UpdateMovieRequestDTO request)
        {
            try
            {
                await _adminMovieRepo.UpdateMovieAsync(movieId, request);
            }
            catch (SqlException ex)
            {
                // Bắt lỗi nghiệp vụ từ SP (Lỗi không tồn tại 53002)
                if (ex.Number == 53002)
                {
                    throw new KeyNotFoundException($"Không tìm thấy phim với ID = {movieId}");
                }
                throw; // Lỗi khác
            }
        }
        public async Task DeleteMovieAsync(int movieId)
        {
            try
            {
                await _adminMovieRepo.DeleteMovieAsync(movieId);
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