using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.BUS;
using MovieBookingAPI.DAO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public class MoviEBUS : IMovieBUS
    {
        private readonly IMovieDAO _movieDAO;

        public MoviEBUS(IMovieDAO movieDAO)
        {
            _movieDAO = movieDAO;
        }

        public async Task<PagedResult<MovieDTO>> GetMoviesAsync(int pageIndex, int pageSize, string? sortBy)
        {
            // Có thể thêm logic validate business tại đây (ví dụ giới hạn PageSize tối đa)
            if (pageSize > 100) pageSize = 100;

            return await _movieDAO.GetMoviesPagedAsync(pageIndex, pageSize, sortBy);
        }
        public async Task<PagedResult<MovieDTO>> SearchMoviesAsync(string? keyword, int? genreId, int? year, int pageIndex, int pageSize)
        {
            // Có thể thêm logic chuẩn hóa từ khóa tại đây (Trim, Lowercase...)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
            }

            return await _movieDAO.SearchMoviesAsync(keyword, genreId, year, pageIndex, pageSize);
        }

        public async Task<List<GenreDTO>> GetAllGenresAsync()
        {
            return await _movieDAO.GetAllGenresAsync();
        }

        public async Task<MovieDetailDTO?> GetMovieDetailAsync(int id)
        {
            // Có thể thêm logic kiểm tra ID > 0 tại đây
            if (id <= 0) return null;

            return await _movieDAO.GetMovieByIdAsync(id);
        }
    }
}