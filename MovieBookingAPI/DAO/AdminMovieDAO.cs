using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBooking.Domain.DTOs;
using Newtonsoft.Json;
using Npgsql; // Thư viện chính cho PostgreSQL
using NpgsqlTypes; // Chứa các kiểu dữ liệu PostgreSQL
using System.Data;

namespace MovieBookingAPI.DAO
{
    public class AdminMovieDAO : IAdminMovieDAO
    {
        private readonly AppDbContext _context;

        public AdminMovieDAO(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. IMPORT MOVIES (BULK INSERT)
        // ==========================================
        public async Task<int> ImportMoviesAsync(string jsonMovies)
        {
            // Sửa tên tham số và kiểu dữ liệu cho khớp
            var p_jsondata = new NpgsqlParameter("p_jsondata", NpgsqlDbType.Jsonb) { Value = jsonMovies };

            _context.Database.SetCommandTimeout(300);

            // Sửa tên function thành chữ thường và cú pháp gọi
            var result = await _context.Database
                .SqlQueryRaw<int>("SELECT * FROM usp_importmoviesbulk(@p_jsondata)", p_jsondata)
                .ToListAsync();

            return result.FirstOrDefault();
        }

        // ==========================================
        // 2. ADD MOVIE
        // ==========================================
        public async Task<int> AddMovieAsync(AddMovieRequestDTO movie)
        {
            string genresJson = JsonConvert.SerializeObject(movie.Genres);
            string actorsJson = JsonConvert.SerializeObject(movie.Casts);

            // Sửa tên tham số thành chữ thường và nhất quán
            var parameters = new[]
            {
        new NpgsqlParameter("p_title", movie.Title),
        new NpgsqlParameter("p_storyline", (object)movie.StoryLine ?? DBNull.Value),
        new NpgsqlParameter("p_director", (object)movie.Director ?? DBNull.Value),
        new NpgsqlParameter("p_duration", movie.Duration),
        new NpgsqlParameter("p_releaseyear", movie.ReleaseYear),
        new NpgsqlParameter("p_agerating", (object)movie.AgeRating ?? DBNull.Value),
        new NpgsqlParameter("p_rating", NpgsqlDbType.Double) { Value = movie.Rating },
        new NpgsqlParameter("p_posterurl", (object)movie.PosterUrl ?? DBNull.Value),
        new NpgsqlParameter("p_status", movie.Status ?? "Coming Soon"),
        new NpgsqlParameter("p_genrenamesjson", NpgsqlDbType.Jsonb) { Value = genresJson },
        new NpgsqlParameter("p_actornamesjson", NpgsqlDbType.Jsonb) { Value = actorsJson }
    };

            // Sửa tên function thành chữ thường và cú pháp gọi SELECT
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "SELECT usp_addmovie(@p_title, @p_storyline, @p_director, @p_duration, @p_releaseyear, @p_agerating, @p_rating, @p_posterurl, @p_status, @p_genrenamesjson, @p_actornamesjson)",
                    parameters
                )
                .ToListAsync();

            return result.FirstOrDefault();
        }

        // ==========================================
        // 3. UPDATE MOVIE
        // ==========================================
        public async Task UpdateMovieAsync(int movieId, UpdateMovieRequestDTO movie)
        {
            string genresJson = JsonConvert.SerializeObject(movie.Genres);
            string actorsJson = JsonConvert.SerializeObject(movie.Casts);

            // Sửa tên tham số cho nhất quán
            var parameters = new[]
            {
        new NpgsqlParameter("p_movieid", movieId),
        new NpgsqlParameter("p_title", movie.Title),
        new NpgsqlParameter("p_storyline", (object)movie.StoryLine ?? DBNull.Value),
        new NpgsqlParameter("p_director", (object)movie.Director ?? DBNull.Value),
        new NpgsqlParameter("p_duration", movie.Duration),
        new NpgsqlParameter("p_releaseyear", movie.ReleaseYear),
        new NpgsqlParameter("p_agerating", (object)movie.AgeRating ?? DBNull.Value),
        new NpgsqlParameter("p_rating", movie.Rating), // Npgsql tự suy luận kiểu Double
        new NpgsqlParameter("p_posterurl", (object)movie.PosterUrl ?? DBNull.Value),
        new NpgsqlParameter("p_status", movie.Status ?? "Coming Soon"),
        new NpgsqlParameter("p_genrenamesjson", NpgsqlDbType.Jsonb) { Value = genresJson },
        new NpgsqlParameter("p_actornamesjson", NpgsqlDbType.Jsonb) { Value = actorsJson }
    };

            // Sửa tên function và cú pháp gọi
            await _context.Database.ExecuteSqlRawAsync(
                "SELECT usp_updatemovie(@p_movieid, @p_title, @p_storyline, @p_director, @p_duration, @p_releaseyear, @p_agerating, @p_rating, @p_posterurl, @p_status, @p_genrenamesjson, @p_actornamesjson)",
                parameters
            );
        }

        // ==========================================
        // 4. DELETE MOVIE
        // ==========================================
        public async Task DeleteMovieAsync(int movieId)
        {
            // Sửa tên tham số
            var p_movieid = new NpgsqlParameter("p_movieid", movieId);

            // Sửa cú pháp gọi Function trả về VOID
            // Dùng SELECT thay vì CALL
            await _context.Database.ExecuteSqlRawAsync(
                "SELECT usp_deletemovie(@p_movieid)",
                p_movieid
            );
        }
    }
}