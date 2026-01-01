using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public interface IMovieDAO
    {
        Task<PagedResult<MovieDTO>> GetMoviesPagedAsync(int pageIndex, int pageSize, string sortBy);
        Task<PagedResult<MovieDTO>> SearchMoviesAsync(string? keyword, int? genreId, int? year, int pageIndex, int pageSize);
        Task<List<GenreDTO>> GetAllGenresAsync();
        Task<MovieDetailDTO?> GetMovieByIdAsync(int id);

    }
}