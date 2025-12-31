using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using Newtonsoft.Json;
using System.Data;
using MovieBookingAPI.Models.DTOs;
namespace MovieBookingAPI.Repositories
{
    public class AdminMovieRepository : IAdminMovieRepository
    {
        private readonly AppDbContext _context;


        public AdminMovieRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<int> ImportMoviesAsync(string jsonMovies)
        {
            var pJsonData = new SqlParameter("@JsonData", SqlDbType.NVarChar, -1) // -1 tương đương MAX
            {
                Value = jsonMovies
            };


            // Tăng timeout vì xử lý file lớn có thể mất thời gian
            _context.Database.SetCommandTimeout(300);


            // Sử dụng SqlQueryRaw để lấy giá trị vô hướng (Scalar) trả về từ SELECT
            var result = await _context.Database
                .SqlQueryRaw<int>("EXEC usp_ImportMoviesBulk @JsonData", pJsonData)
                .ToListAsync();


            return result.FirstOrDefault();
        }
        public async Task<int> AddMovieAsync(AddMovieRequestDto movie)
        {
            // 1. Serialize danh sách thể loại và diễn viên sang JSON String
            string genresJson = JsonConvert.SerializeObject(movie.Genres);
            string actorsJson = JsonConvert.SerializeObject(movie.Casts);


            // 2. Khởi tạo tham số
            var parameters = new[]
            {
                new SqlParameter("@Title", movie.Title),
                new SqlParameter("@StoryLine", movie.StoryLine ?? (object)DBNull.Value),
                new SqlParameter("@Director", movie.Director ?? (object)DBNull.Value),
                new SqlParameter("@Duration", movie.Duration),
                new SqlParameter("@ReleaseYear", movie.ReleaseYear),
                new SqlParameter("@AgeRating", movie.AgeRating ?? (object)DBNull.Value),
                new SqlParameter("@Rating", movie.Rating),
                new SqlParameter("@PosterUrl", movie.PosterUrl ?? (object)DBNull.Value),
                new SqlParameter("@Status", movie.Status ?? "Coming Soon"),
                new SqlParameter("@GenreNamesJson", genresJson),
                new SqlParameter("@ActorNamesJson", actorsJson)
            };


            // 3. Thực thi SP và lấy về Scalar (NewMovieId)
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "EXEC usp_AddMovie @Title, @StoryLine, @Director, @Duration, @ReleaseYear, @AgeRating, @Rating, @PosterUrl, @Status, @GenreNamesJson, @ActorNamesJson",
                    parameters
                )
                .ToListAsync();


            return result.FirstOrDefault();
        }
        public async Task UpdateMovieAsync(int movieId, UpdateMovieRequestDto movie)
        {
            string genresJson = JsonConvert.SerializeObject(movie.Genres);
            string actorsJson = JsonConvert.SerializeObject(movie.Casts);


            var parameters = new[]
            {
        new SqlParameter("@MovieId", movieId),
        new SqlParameter("@Title", movie.Title),
        new SqlParameter("@StoryLine", movie.StoryLine ?? (object)DBNull.Value),
        new SqlParameter("@Director", movie.Director ?? (object)DBNull.Value),
        new SqlParameter("@Duration", movie.Duration),
        new SqlParameter("@ReleaseYear", movie.ReleaseYear),
        new SqlParameter("@AgeRating", movie.AgeRating ?? (object)DBNull.Value),
        new SqlParameter("@Rating", movie.Rating),
        new SqlParameter("@PosterUrl", movie.PosterUrl ?? (object)DBNull.Value),
        new SqlParameter("@Status", movie.Status ?? "Coming Soon"),
        new SqlParameter("@GenreNamesJson", genresJson),
        new SqlParameter("@ActorNamesJson", actorsJson)
    };


            // Sử dụng ExecuteSqlRawAsync vì không cần giá trị trả về (void)
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC usp_UpdateMovie @MovieId, @Title, @StoryLine, @Director, @Duration, @ReleaseYear, @AgeRating, @Rating, @PosterUrl, @Status, @GenreNamesJson, @ActorNamesJson",
                parameters
            );
        }
        public async Task DeleteMovieAsync(int movieId)
        {
            var parameter = new SqlParameter("@MovieId", movieId);


            // Sử dụng ExecuteSqlRawAsync
            await _context.Database.ExecuteSqlRawAsync("EXEC usp_DeleteMovie @MovieId", parameter);
        }

    }
}
