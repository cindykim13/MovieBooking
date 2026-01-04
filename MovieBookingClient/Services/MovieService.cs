using MovieBooking.Domain.DTOs;
using RestSharp;
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class MovieService : BaseApiService
    {
        public MovieService() : base() { }

        public async Task<PagedResult<MovieDTO>> GetMoviesAsync(int pageIndex = 1, int pageSize = 10)
        {
            var request = CreateRequest("/api/movies", Method.Get);
            request.AddParameter("pageIndex", pageIndex);
            request.AddParameter("pageSize", pageSize);

            return await ExecuteAsync<PagedResult<MovieDTO>>(request);
        }
        // [THÊM PHƯƠNG THỨC MỚI TẠI ĐÂY]
        public async Task<PagedResult<MovieDTO>> SearchMoviesAsync(string keyword, string status, int? genreId, int? year, int pageIndex, int pageSize)
        {
            var request = CreateRequest("/api/movies/search", Method.Get);

            // Thêm các tham số vào Query String
            if (!string.IsNullOrWhiteSpace(keyword))
                request.AddParameter("keyword", keyword);
            if (!string.IsNullOrWhiteSpace(status))
                request.AddParameter("status", status);
            if (genreId.HasValue)
                request.AddParameter("genreId", genreId.Value);
            if (year.HasValue)
                request.AddParameter("year", year.Value);

            request.AddParameter("pageIndex", pageIndex);
            request.AddParameter("pageSize", pageSize);

            return await ExecuteAsync<PagedResult<MovieDTO>>(request);
        }
        // [THÊM MỚI TẠI ĐÂY]
        public async Task<List<GenreDTO>> GetAllGenresAsync()
        {
            // Tạo request đến API lấy danh sách thể loại
            var request = CreateRequest("/api/movies/genres", Method.Get);

            // Gọi và chờ kết quả
            return await ExecuteAsync<List<GenreDTO>>(request);
        }
        // Trong MovieService.cs
        public async Task<MovieDetailDTO> GetMovieDetailAsync(int id)
        {
            var request = CreateRequest($"/api/movies/{id}", Method.Get);
            return await ExecuteAsync<MovieDetailDTO>(request);
        }
    }
}