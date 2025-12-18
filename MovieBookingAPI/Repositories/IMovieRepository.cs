using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<PagedResult<MovieDto>> GetMoviesPagedAsync(int pageIndex, int pageSize, string sortBy);
        Task<PagedResult<MovieDto>> SearchMoviesAsync(string? keyword, int? genreId, int? year, int pageIndex, int pageSize);
        Task<List<GenreDto>> GetAllGenresAsync();
        Task<MovieDetailDto> GetMovieByIdAsync(int id);

    }
}