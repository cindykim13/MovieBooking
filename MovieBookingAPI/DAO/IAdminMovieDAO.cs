using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;
namespace MovieBookingAPI.DAO
{
    public interface IAdminMovieDAO
    {
        Task<int> ImportMoviesAsync(string jsonMovies);
        // Hàm thêm mới phim lẻ
        Task<int> AddMovieAsync(AddMovieRequestDTO movie);
        Task UpdateMovieAsync(int movieId, UpdateMovieRequestDTO movie);
        Task DeleteMovieAsync(int movieId);
    }
}
