using Microsoft.AspNetCore.Http;
using MovieBookingAPI.Models.DTOs;
using System.Threading.Tasks;
namespace MovieBookingAPI.Services
{
    public interface IAdminMovieService
    {
        // Input là IFormFile (file upload từ client)
        Task<int> ImportMoviesFromFileAsync(IFormFile file);
        Task<int> AddMovieAsync(AddMovieRequestDto request);

        Task UpdateMovieAsync(int movieId, UpdateMovieRequestDto request);

        Task DeleteMovieAsync(int movieId);

    }
}
