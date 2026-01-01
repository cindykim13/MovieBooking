using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.DTOs;
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
            // PostgreSQL: Dùng NpgsqlParameter và kiểu Jsonb hoặc Text cho JSON
            var pJsonData = new NpgsqlParameter("@p_json_data", NpgsqlDbType.Jsonb)
            {
                Value = jsonMovies
            };

            // Tăng timeout cho tác vụ nặng
            _context.Database.SetCommandTimeout(300);

            // PostgreSQL: Gọi Procedure dùng lệnh 'CALL'.
            // Lưu ý: Procedure trong PG không return giá trị trực tiếp qua SELECT như SQL Server.
            // Để lấy số lượng insert, ta thường dùng Function trả về void hoặc int.
            // Giả sử usp_import_movies_bulk là một FUNCTION trả về integer (số dòng insert).
            // Cú pháp: SELECT * FROM func_name(...)

            var result = await _context.Database
                .SqlQueryRaw<int>("SELECT * FROM usp_import_movies_bulk(@p_json_data)", pJsonData)
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

            // PostgreSQL: Định nghĩa tham số với NpgsqlParameter
            // Lưu ý: Tên tham số trong PG thường viết thường (snake_case)
            var parameters = new[]
            {
                new NpgsqlParameter("@p_title", movie.Title),
                new NpgsqlParameter("@p_story_line", (object)movie.StoryLine ?? DBNull.Value),
                new NpgsqlParameter("@p_director", (object)movie.Director ?? DBNull.Value),
                new NpgsqlParameter("@p_duration", movie.Duration),
                new NpgsqlParameter("@p_release_year", movie.ReleaseYear),
                new NpgsqlParameter("@p_age_rating", (object)movie.AgeRating ?? DBNull.Value),
                // PG: float trong C# tương ứng với double precision hoặc real
                new NpgsqlParameter("@p_rating", NpgsqlDbType.Double) { Value = movie.Rating },
                new NpgsqlParameter("@p_poster_url", (object)movie.PosterUrl ?? DBNull.Value),
                new NpgsqlParameter("@p_status", movie.Status ?? "Coming Soon"),
                // PG: JSON truyền vào dưới dạng Jsonb
                new NpgsqlParameter("@p_genre_names_json", NpgsqlDbType.Jsonb) { Value = genresJson },
                new NpgsqlParameter("@p_actor_names_json", NpgsqlDbType.Jsonb) { Value = actorsJson }
            };

            // Gọi Function trả về ID (Scalar)
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "SELECT * FROM usp_add_movie(@p_title, @p_story_line, @p_director, @p_duration, @p_release_year, @p_age_rating, @p_rating, @p_poster_url, @p_status, @p_genre_names_json, @p_actor_names_json)",
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

            var parameters = new[]
            {
                new NpgsqlParameter("@p_movie_id", movieId),
                new NpgsqlParameter("@p_title", movie.Title),
                new NpgsqlParameter("@p_story_line", (object)movie.StoryLine ?? DBNull.Value),
                new NpgsqlParameter("@p_director", (object)movie.Director ?? DBNull.Value),
                new NpgsqlParameter("@p_duration", movie.Duration),
                new NpgsqlParameter("@p_release_year", movie.ReleaseYear),
                new NpgsqlParameter("@p_age_rating", (object)movie.AgeRating ?? DBNull.Value),
                new NpgsqlParameter("@p_rating", NpgsqlDbType.Double) { Value = movie.Rating },
                new NpgsqlParameter("@p_poster_url", (object)movie.PosterUrl ?? DBNull.Value),
                new NpgsqlParameter("@p_status", movie.Status ?? "Coming Soon"),
                new NpgsqlParameter("@p_genre_names_json", NpgsqlDbType.Jsonb) { Value = genresJson },
                new NpgsqlParameter("@p_actor_names_json", NpgsqlDbType.Jsonb) { Value = actorsJson }
            };

            // PostgreSQL: Dùng CALL cho Procedure (không trả về dữ liệu)
            await _context.Database.ExecuteSqlRawAsync(
                "CALL usp_update_movie(@p_movie_id, @p_title, @p_story_line, @p_director, @p_duration, @p_release_year, @p_age_rating, @p_rating, @p_poster_url, @p_status, @p_genre_names_json, @p_actor_names_json)",
                parameters
            );
        }

        // ==========================================
        // 4. DELETE MOVIE
        // ==========================================
        public async Task DeleteMovieAsync(int movieId)
        {
            var parameter = new NpgsqlParameter("@p_movie_id", movieId);

            // PostgreSQL: Dùng CALL cho Procedure
            await _context.Database.ExecuteSqlRawAsync("CALL usp_delete_movie(@p_movie_id)", parameter);
        }
    }
}