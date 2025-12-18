using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Repositories;

namespace MovieBookingAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;

        public MovieService(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<PagedResult<MovieDto>> GetMoviesAsync(int pageIndex, int pageSize, string sortBy)
        {
            // Có thể thêm logic validate business tại đây (ví dụ giới hạn PageSize tối đa)
            if (pageSize > 100) pageSize = 100;

            return await _movieRepo.GetMoviesPagedAsync(pageIndex, pageSize, sortBy);
        }
        public async Task<PagedResult<MovieDto>> SearchMoviesAsync(string? keyword, int? genreId, int? year, int pageIndex, int pageSize)
        {
            // Có thể thêm logic chuẩn hóa từ khóa tại đây (Trim, Lowercase...)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
            }

            return await _movieRepo.SearchMoviesAsync(keyword, genreId, year, pageIndex, pageSize);
        }

        public async Task<List<GenreDto>> GetAllGenresAsync()
        {
            return await _movieRepo.GetAllGenresAsync();
        }

        public async Task<MovieDetailDto> GetMovieDetailAsync(int id)
        {
            // Có thể thêm logic kiểm tra ID > 0 tại đây
            if (id <= 0) return null;

            return await _movieRepo.GetMovieByIdAsync(id);
        }
    }
}