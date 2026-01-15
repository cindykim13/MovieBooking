using Npgsql;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBooking.Domain.DTOs;
using System.Data;
using MovieBookingAPI.Models.Entities;
using System.Linq;

namespace MovieBookingAPI.DAO
{
    public class MovieDAO : IMovieDAO
    {
        private readonly AppDbContext _context;

        public MovieDAO(AppDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách phim phân trang
        public async Task<PagedResult<MovieDTO>> GetMoviesPagedAsync(int pageIndex, int pageSize, string? sortBy)
        {
            var p_pageindex = new NpgsqlParameter("p_pageindex", pageIndex);
            var p_pagesize = new NpgsqlParameter("p_pagesize", pageSize);
            var p_sortby = new NpgsqlParameter("p_sortby", sortBy ?? "releaseyear");

            var result = await _context.Set<PagedMovieResult>()
                .FromSqlRaw("SELECT * FROM usp_getmoviespaged(@p_pageindex, @p_pagesize, @p_sortby)",
                    p_pageindex, p_pagesize, p_sortby)
                .ToListAsync();

            return new PagedResult<MovieDTO>
            {
                Items = result.Select(r => new MovieDTO
                {
                    Id = r.movieid,
                    Title = r.title,
                    Duration = r.duration,
                    ReleaseYear = r.releaseyear,
                    Rating = r.rating,
                    PosterUrl = r.posterurl ?? string.Empty,
                    Status = r.status ?? string.Empty,
                    Genres = string.IsNullOrEmpty(r.genres)
                             ? new List<string>()
                             : r.genres.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(g => g.Trim())
                                     .ToList()
                }).ToList(),
                TotalRecords = (int)(result.FirstOrDefault()?.totalcount ?? 0),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        // 2. Tìm kiếm phim
        public async Task<PagedResult<MovieDTO>> SearchMoviesAsync(string? keyword, string? status, int? genreId, int? year, int pageIndex, int pageSize)
        {
            var p_keyword = new NpgsqlParameter("p_keyword", string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : keyword);
            var p_status = new NpgsqlParameter("p_status", string.IsNullOrWhiteSpace(status) ? DBNull.Value : status);
            var p_genreid = new NpgsqlParameter("p_genreid", genreId.HasValue ? (object)genreId.Value : DBNull.Value);
            var p_releaseyear = new NpgsqlParameter("p_releaseyear", year.HasValue ? (object)year.Value : DBNull.Value);
            var p_pageindex = new NpgsqlParameter("p_pageindex", pageIndex);
            var p_pagesize = new NpgsqlParameter("p_pagesize", pageSize);

            var rawResult = await _context.Set<PagedMovieResult>()
                 .FromSqlRaw("SELECT * FROM usp_searchmovies(@p_keyword, @p_status, @p_genreid, @p_releaseyear, @p_pageindex, @p_pagesize)",
                    p_keyword, p_status, p_genreid, p_releaseyear, p_pageindex, p_pagesize)
                .ToListAsync();

            var pagedResult = new PagedResult<MovieDTO>
            {
                Items = rawResult.Select(r => new MovieDTO
                {
                    Id = r.movieid,
                    Title = r.title,
                    Duration = r.duration,
                    ReleaseYear = r.releaseyear,
                    Rating = r.rating,
                    PosterUrl = r.posterurl ?? string.Empty,
                    Status = r.status ?? string.Empty,
                    Genres = string.IsNullOrEmpty(r.genres)
                             ? new List<string>()
                             : r.genres.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(g => g.Trim())
                                     .ToList()
                }).ToList(),
                TotalRecords = (int)(rawResult.FirstOrDefault()?.totalcount ?? 0),
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return pagedResult;
        }

        // 3. Lấy tất cả thể loại
        public async Task<List<GenreDTO>> GetAllGenresAsync()
        {
            return await _context.Set<Genre>()
                .Select(g => new GenreDTO
                {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName
                })
                .OrderBy(g => g.GenreName)
                .ToListAsync();
        }

        // 4. Lấy chi tiết phim
        public async Task<MovieDetailDTO?> GetMovieByIdAsync(int id)
        {
            var p_movieid = new NpgsqlParameter("p_movieid", id);

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
                Genres = result.genres?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(x => x.Trim()).ToList() ?? new List<string>(),
                Casts = result.casts?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(x => x.Trim()).ToList() ?? new List<string>()
            };
        }

        // 5. Cập nhật phim (NEW)
        public async Task UpdateMovieAsync(int id, UpdateMovieRequestDTO request)
        {
            // 1. Khai báo các tham số (Parameter)
            var p_movieid = new NpgsqlParameter("p_movieid", id);
            var p_title = new NpgsqlParameter("p_title", request.Title);
            var p_storyline = new NpgsqlParameter("p_storyline", request.StoryLine ?? (object)DBNull.Value);
            var p_duration = new NpgsqlParameter("p_duration", request.Duration);

            // SỬA LỖI CS0019: Nếu ReleaseYear là int?, dùng ??. Nếu là int, bỏ ?? đi.
            // Dưới đây giả định bạn ĐÃ sửa DTO thành int? như hướng dẫn trước.
            // Nếu chưa sửa DTO, hãy dùng: request.ReleaseYear (bỏ đoạn ?? phía sau)
            var p_releaseyear = new NpgsqlParameter("p_releaseyear", request.ReleaseYear ?? (object)DBNull.Value);

            // SỬA LỖI CS0103: Thêm dòng này vì bạn đang bị thiếu biến p_rating
            var p_rating = new NpgsqlParameter("p_rating", request.Rating);

            var p_posterurl = new NpgsqlParameter("p_posterurl", request.PosterUrl ?? (object)DBNull.Value);

            // 2. Câu lệnh SQL
            string sql = "CALL usp_updatemovie(@p_movieid, @p_title, @p_storyline, @p_duration, @p_releaseyear, @p_rating, @p_posterurl)";

            // 3. Thực thi (Lúc này p_rating đã được định nghĩa ở trên nên sẽ hết lỗi đỏ)
            await _context.Database.ExecuteSqlRawAsync(sql,
                p_movieid, p_title, p_storyline, p_duration, p_releaseyear, p_rating, p_posterurl);
        }

        // 6. Xóa phim (NEW)
        public async Task DeleteMovieAsync(int id)
        {
            var p_movieid = new NpgsqlParameter("p_movieid", id);
            string sql = "CALL usp_deletemovie(@p_movieid)";
            await _context.Database.ExecuteSqlRawAsync(sql, p_movieid);
        }
    }
}