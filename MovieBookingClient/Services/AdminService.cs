using MovieBooking.Domain.DTOs; // Sử dụng DTO từ project Shared
using RestSharp;
using System.IO; // Thêm using này để sử dụng Stream
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class AdminService : BaseApiService
    {
        public AdminService() : base() { }

        // --- QUẢN LÝ PHIM ---

        public async Task<int> AddMovieAsync(AddMovieRequestDTO movie)
        {
            var request = CreateRequest("/api/admin/movies", Method.Post);
            request.AddBody(movie);
            var result = await ExecuteAsync<dynamic>(request);
            return (result != null) ? (int)result.movieId : 0;
        }

        public async Task UpdateMovieAsync(int movieId, UpdateMovieRequestDTO movie)
        {
            var request = CreateRequest($"/api/admin/movies/{movieId}", Method.Put);
            request.AddBody(movie);
            await ExecuteAsync<dynamic>(request); // Không cần hứng kết quả nếu API trả về 200 OK
        }

        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            var request = CreateRequest($"/api/admin/movies/{movieId}", Method.Delete);
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }
        public async Task<int> ImportMoviesFromFileAsync(Stream fileStream, string fileName)
        {
            var request = CreateRequest("/api/admin/movies/import", Method.Post);

            // RestSharp sử dụng AddFile với Stream
            request.AddFile("file", () => fileStream, fileName, "application/json");

            // API trả về { message, movieId }, cần một DTO hoặc dynamic để hứng
            var result = await ExecuteAsync<dynamic>(request);

            if (result != null && result.count != null)
            {
                return (int)result.count;
            }
            return 0;
        }
    }
}