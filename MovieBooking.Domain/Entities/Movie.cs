using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.Domain.Entities // Hoặc MovieBookingAPI.Models.Entities (nhớ đồng bộ namespace)
{
    [Table("movie")] // Map với bảng movie trong PostgreSQL
    public class Movie
    {
        [Key]
        [Column("movieid")]
        public int MovieId { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("duration")]
        public int Duration { get; set; }

        // Thêm các cột khác nếu DB có (ví dụ: Description, ReleaseDate...)
        // Nhưng tối thiểu phải có MovieId và Title để code DAO chạy được.
    }
}