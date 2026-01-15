using MovieBooking.Domain.DTOs;
using MovieBookingClient.Session;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http; // Giữ lại theo yêu cầu của bạn
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class MovieService : BaseApiService
    {
        public MovieService() : base() { }

        // --- HÀM MỚI BỔ SUNG ĐỂ FIX LỖI CS1061 ---
        // Sử dụng ExecuteAsync chuẩn RestSharp của project bạn
        public async Task<List<MovieDTO>> GetAllMoviesAsync()
        {
            var request = CreateRequest("api/admin/movies", Method.Get);
            var result = await ExecuteAsync<List<MovieDTO>>(request);
            return result ?? new List<MovieDTO>();
        }

        // 1. Lấy danh sách phim phân trang
        public async Task<PagedResult<MovieDTO>> GetMoviesAsync(int pageIndex = 1, int pageSize = 10)
        {
            var request = CreateRequest("api/Movies", Method.Get);
            request.AddQueryParameter("pageIndex", pageIndex.ToString());
            request.AddQueryParameter("pageSize", pageSize.ToString());

            return await ExecuteAsync<PagedResult<MovieDTO>>(request);
        }

        // 2. Tìm kiếm phim
        public async Task<PagedResult<MovieDTO>> SearchMoviesAsync(string keyword, string status, int? genreId, int? year, int pageIndex, int pageSize)
        {
            var request = CreateRequest("api/Movies/search", Method.Get);

            if (!string.IsNullOrWhiteSpace(keyword))
                request.AddQueryParameter("keyword", keyword);
            if (!string.IsNullOrWhiteSpace(status))
                request.AddQueryParameter("status", status);
            if (genreId.HasValue)
                request.AddQueryParameter("genreId", genreId.Value.ToString());
            if (year.HasValue)
                request.AddQueryParameter("year", year.Value.ToString());

            request.AddQueryParameter("pageIndex", pageIndex.ToString());
            request.AddQueryParameter("pageSize", pageSize.ToString());

            return await ExecuteAsync<PagedResult<MovieDTO>>(request);
        }

        // 3. Lấy tất cả thể loại
        public async Task<List<GenreDTO>> GetAllGenresAsync()
        {
            var request = CreateRequest("api/Movies/genres", Method.Get);
            return await ExecuteAsync<List<GenreDTO>>(request);
        }

        // 4. Lấy chi tiết một bộ phim
        public async Task<MovieDetailDTO> GetMovieDetailAsync(int id)
        {
            var request = CreateRequest($"api/Movies/{id}", Method.Get);
            return await ExecuteAsync<MovieDetailDTO>(request);
        }

        // 5. Thêm phim mới
        public async Task<bool> CreateMovieAsync(MovieDTO movie)
        {
            var request = CreateRequest("api/admin/movies", Method.Post);
            request.AddJsonBody(movie);

            var result = await ExecuteAsync<object>(request);
            return result != null;
        }

        // 6. Cập nhật phim
        public async Task<bool> UpdateMovieAsync(int id, UpdateMovieRequestDTO movie)
        {
            var request = CreateRequest($"api/admin/movies/{id}", Method.Put);
            request.AddJsonBody(movie);

            var result = await ExecuteAsync<object>(request);
            return result != null;
        }

        // 7. Xóa phim
        public async Task<bool> DeleteMovieAsync(int id)
        {
            var request = CreateRequest($"/api/admin/movies/{id}", Method.Delete);

            var result = await ExecuteAsync<object>(request);
            return result != null;
        }
    }
}