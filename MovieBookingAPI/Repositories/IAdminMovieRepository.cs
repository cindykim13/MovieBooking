using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;


namespace MovieBookingAPI.Repositories
{
    public interface IAdminMovieRepository
    {
        Task<int> ImportMoviesAsync(string jsonMovies);

        // Hàm thêm mới phim lẻ
        Task<int> AddMovieAsync(AddMovieRequestDto movie);

        Task UpdateMovieAsync(int movieId, UpdateMovieRequestDto movie);

        Task DeleteMovieAsync(int movieId);
    }
}
