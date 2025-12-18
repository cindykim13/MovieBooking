using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.DTOs;
using System.Data;
using MovieBookingAPI.Models.Entities;

namespace MovieBookingAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<MovieDto>> GetMoviesPagedAsync(int pageIndex, int pageSize, string sortBy)
        {
            var result = new PagedResult<MovieDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            // Sử dụng ADO.NET để xử lý Output Parameter hiệu quả
            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "usp_GetMoviesPaged";
                command.CommandType = CommandType.StoredProcedure;

                // Input Parameters
                command.Parameters.Add(new SqlParameter("@PageIndex", pageIndex));
                command.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                command.Parameters.Add(new SqlParameter("@SortBy", sortBy ?? "ReleaseYear"));

                // Output Parameter
                var totalParam = new SqlParameter("@TotalRecords", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(totalParam);

                // Execute Reader
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Items.Add(new MovieDto
                        {
                            MovieId = reader.GetInt32(reader.GetOrdinal("MovieId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),

                            // [FIX]: Kiểm tra Null cho Duration
                            Duration = reader.IsDBNull(reader.GetOrdinal("Duration")) ? 0 : reader.GetInt32(reader.GetOrdinal("Duration")),

                            // [FIX]: Kiểm tra Null cho ReleaseYear
                            ReleaseYear = reader.IsDBNull(reader.GetOrdinal("ReleaseYear")) ? 0 : reader.GetInt32(reader.GetOrdinal("ReleaseYear")),

                            // [FIX LỖI CHÍNH]: Kiểm tra Null cho Rating trước khi GetDouble
                            Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("Rating")),

                            // Các trường chuỗi đã được xử lý Null trước đó, giữ nguyên
                            PosterUrl = reader.IsDBNull(reader.GetOrdinal("PosterUrl")) ? null : reader.GetString(reader.GetOrdinal("PosterUrl")),
                            Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status")),
                            Genres = reader.IsDBNull(reader.GetOrdinal("Genres")) ? null : reader.GetString(reader.GetOrdinal("Genres"))
                        });
                    }
                }

                // Đọc giá trị Output Parameter sau khi đóng Reader
                if (totalParam.Value != DBNull.Value)
                {
                    result.TotalRecords = (int)totalParam.Value;
                }
            }

            return result;
        }
        public async Task<PagedResult<MovieDto>> SearchMoviesAsync(string? keyword, int? genreId, int? year, int pageIndex, int pageSize)
        {
            var result = new PagedResult<MovieDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "usp_SearchMovies";
                command.CommandType = CommandType.StoredProcedure;

                // Xử lý tham số Nullable
                command.Parameters.Add(new SqlParameter("@Keyword", string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : keyword));
                command.Parameters.Add(new SqlParameter("@GenreId", genreId.HasValue ? genreId.Value : DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ReleaseYear", year.HasValue ? year.Value : DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PageIndex", pageIndex));
                command.Parameters.Add(new SqlParameter("@PageSize", pageSize));

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // Đọc TotalCount từ dòng đầu tiên (giá trị này lặp lại ở mọi dòng)
                        if (result.TotalRecords == 0)
                        {
                            result.TotalRecords = reader.GetInt32(reader.GetOrdinal("TotalCount"));
                        }

                        result.Items.Add(new MovieDto
                        {
                            MovieId = reader.GetInt32(reader.GetOrdinal("MovieId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            // Áp dụng kỹ thuật kiểm tra NULL an toàn như bài trước
                            Duration = reader.IsDBNull(reader.GetOrdinal("Duration")) ? 0 : reader.GetInt32(reader.GetOrdinal("Duration")),
                            ReleaseYear = reader.IsDBNull(reader.GetOrdinal("ReleaseYear")) ? 0 : reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                            Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("Rating")),
                            PosterUrl = reader.IsDBNull(reader.GetOrdinal("PosterUrl")) ? null : reader.GetString(reader.GetOrdinal("PosterUrl")),
                            Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status")),
                            Genres = reader.IsDBNull(reader.GetOrdinal("Genres")) ? null : reader.GetString(reader.GetOrdinal("Genres"))
                        });
                    }
                }
            }

            return result;
        }

        public async Task<List<GenreDto>> GetAllGenresAsync()
        {
            // Sử dụng EF Core LINQ để truy vấn nhanh bảng Genre
            // Không cần Stored Procedure cho câu lệnh SELECT đơn giản này
            return await _context.Set<Genre>()
                .Select(g => new GenreDto
                {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName
                })
                .OrderBy(g => g.GenreName)
                .ToListAsync();
        }
        public async Task<MovieDetailDto> GetMovieByIdAsync(int id)
        {
            MovieDetailDto movie = null;

            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "usp_GetMovieDetail";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@MovieId", id));

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        movie = new MovieDetailDto
                        {
                            MovieId = reader.GetInt32(reader.GetOrdinal("MovieId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            // Kiểm tra DBNull cho các trường không bắt buộc hoặc có thể null
                            StoryLine = reader.IsDBNull(reader.GetOrdinal("StoryLine")) ? null : reader.GetString(reader.GetOrdinal("StoryLine")),
                            Director = reader.IsDBNull(reader.GetOrdinal("Director")) ? null : reader.GetString(reader.GetOrdinal("Director")),
                            Duration = reader.IsDBNull(reader.GetOrdinal("Duration")) ? 0 : reader.GetInt32(reader.GetOrdinal("Duration")),
                            ReleaseYear = reader.IsDBNull(reader.GetOrdinal("ReleaseYear")) ? 0 : reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                            AgeRating = reader.IsDBNull(reader.GetOrdinal("AgeRating")) ? null : reader.GetString(reader.GetOrdinal("AgeRating")),
                            Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("Rating")),
                            PosterUrl = reader.IsDBNull(reader.GetOrdinal("PosterUrl")) ? null : reader.GetString(reader.GetOrdinal("PosterUrl")),
                            Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status"))
                        };

                        // Xử lý tách chuỗi Genres (VD: "Action, Drama" -> ["Action", "Drama"])
                        if (!reader.IsDBNull(reader.GetOrdinal("Genres")))
                        {
                            string genresStr = reader.GetString(reader.GetOrdinal("Genres"));
                            movie.Genres = genresStr.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        }

                        // Xử lý tách chuỗi Casts
                        if (!reader.IsDBNull(reader.GetOrdinal("Casts")))
                        {
                            string castsStr = reader.GetString(reader.GetOrdinal("Casts"));
                            movie.Casts = castsStr.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        }
                    }
                }
            }
            return movie;
        }
    }
}