using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.Services
{
    public interface IMovieService
    {
        Task<PagedResult<MovieDto>> GetMoviesAsync(int pageIndex, int pageSize, string sortBy);
        Task<PagedResult<MovieDto>> SearchMoviesAsync(string? keyword, int? genreId, int? year, int pageIndex, int pageSize);
        Task<List<GenreDto>> GetAllGenresAsync();
        Task<MovieDetailDto> GetMovieDetailAsync(int id);

    }
}