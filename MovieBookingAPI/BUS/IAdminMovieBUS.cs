using Microsoft.AspNetCore.Http;
using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;
namespace MovieBookingAPI.BUS
{
    public interface IAdminMovieBUS
    {
        // Input là IFormFile (file upload từ client)
        Task<int> ImportMoviesFromFileAsync(IFormFile file);
        Task<int> AddMovieAsync(AddMovieRequestDTO request);
        Task UpdateMovieAsync(int movieId, UpdateMovieRequestDTO request);
        Task DeleteMovieAsync(int movieId);
    }
}
