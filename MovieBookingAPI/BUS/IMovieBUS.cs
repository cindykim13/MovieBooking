using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public interface IMovieBUS
    {
        // Sửa lại chữ ký hàm
        Task<PagedResult<MovieDTO>> GetMoviesAsync(int pageIndex, int pageSize, string? sortBy);
        Task<PagedResult<MovieDTO>> SearchMoviesAsync(string? keyword, int? genreId, int? year, int pageIndex, int pageSize);
        Task<List<GenreDTO>> GetAllGenresAsync();
        Task<MovieDetailDTO?> GetMovieDetailAsync(int id);

    }
}