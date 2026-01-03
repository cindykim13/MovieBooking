using Npgsql;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBooking.Domain.DTOs;
using System.Data;
using MovieBookingAPI.Models.Entities;

namespace MovieBookingAPI.DAO
{
    public class MovieDAO : IMovieDAO
    {
        private readonly AppDbContext _context;

        public MovieDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<MovieDTO>> GetMoviesPagedAsync(int pageIndex, int pageSize, string? sortBy)
        {
            var p_pageindex = new NpgsqlParameter("p_pageindex", pageIndex);
            var p_pagesize = new NpgsqlParameter("p_pagesize", pageSize);
            var p_sortby = new NpgsqlParameter("p_sortby", sortBy ?? "releaseyear");

            // SỬA LỖI: Chuyển sang FromSqlRaw để gọi Function PostgreSQL
            var result = await _context.Set<PagedMovieResult>()
                .FromSqlRaw("SELECT * FROM usp_getmoviespaged(@p_pageindex, @p_pagesize, @p_sortby)",
                    p_pageindex, p_pagesize, p_sortby)
                .ToListAsync();

            return new PagedResult<MovieDTO>
            {
                Items = result.Select(r => new MovieDTO
                {
                    MovieId = r.movieid,
                    Title = r.title,
                    Duration = r.duration,
                    ReleaseYear = r.releaseyear,
                    Rating = r.rating,
                    PosterUrl = r.posterurl ?? string.Empty,
                    Status = r.status ?? string.Empty,
                    Genres = r.genres ?? string.Empty
                }).ToList(),
                TotalRecords = (int)(result.FirstOrDefault()?.totalcount ?? 0),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
        public async Task<PagedResult<MovieDTO>> SearchMoviesAsync(string? keyword, string? status, int? genreId, int? year, int pageIndex, int pageSize)
        {
            // Định nghĩa các tham số
            var p_keyword = new NpgsqlParameter("p_keyword", string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : keyword);
            var p_status = new NpgsqlParameter("p_status", string.IsNullOrWhiteSpace(status) ? DBNull.Value : status); // Thêm tham số status
            var p_genreid = new NpgsqlParameter("p_genreid", genreId.HasValue ? (object)genreId.Value : DBNull.Value);
            var p_releaseyear = new NpgsqlParameter("p_releaseyear", year.HasValue ? (object)year.Value : DBNull.Value);
            var p_pageindex = new NpgsqlParameter("p_pageindex", pageIndex);
            var p_pagesize = new NpgsqlParameter("p_pagesize", pageSize);

            // Cập nhật câu lệnh gọi
            var rawResult = await _context.Set<PagedMovieResult>()
                 .FromSqlRaw("SELECT * FROM usp_searchmovies(@p_keyword, @p_status, @p_genreid, @p_releaseyear, @p_pageindex, @p_pagesize)",
                    p_keyword, p_status, p_genreid, p_releaseyear, p_pageindex, p_pagesize) // Thêm tham số status vào đây
                .ToListAsync();

            // Chuyển đổi từ lớp tạm (với tên cột chữ thường) sang DTO (với tên thuộc tính PascalCase)
            var pagedResult = new PagedResult<MovieDTO>
            {
                Items = rawResult.Select(r => new MovieDTO
                {
                    MovieId = r.movieid,
                    Title = r.title,
                    Duration = r.duration,
                    ReleaseYear = r.releaseyear,
                    Rating = r.rating,
                    PosterUrl = r.posterurl ?? string.Empty,
                    Status = r.status ?? string.Empty,
                    Genres = r.genres ?? string.Empty
                }).ToList(),
                TotalRecords = (int)(rawResult.FirstOrDefault()?.totalcount ?? 0),
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return pagedResult;
        }

        public async Task<List<GenreDTO>> GetAllGenresAsync()
        {
            // Sử dụng EF Core LINQ để truy vấn nhanh bảng Genre
            // Không cần Stored Procedure cho câu lệnh SELECT đơn giản này
            return await _context.Set<Genre>()
                .Select(g => new GenreDTO
                {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName
                })
                .OrderBy(g => g.GenreName)
                .ToListAsync();
        }
        public async Task<MovieDetailDTO?> GetMovieByIdAsync(int id)
        {
            var p_movieid = new NpgsqlParameter("p_movieid", id);

            // Cần tạo một lớp tạm để hứng kết quả
            var result = await _context.Set<MovieDetailRawResult>()
                .FromSqlRaw("SELECT * FROM usp_getmoviedetail(@p_movieid)", p_movieid)
                .FirstOrDefaultAsync();

            if (result == null) return null;

            return new MovieDetailDTO
            {
                MovieId = result.movieid,
                Title = result.title,
                StoryLine = result.storyline,
                Director = result.director,
                Duration = result.duration,
                ReleaseYear = result.releaseyear,
                AgeRating = result.agerating,
                Rating = result.rating,
                PosterUrl = result.posterurl,
                Status = result.status,
                Genres = result.genres?.Split(", ").ToList() ?? new List<string>(),
                Casts = result.casts?.Split(", ").ToList() ?? new List<string>()
            };
        }
    }
}