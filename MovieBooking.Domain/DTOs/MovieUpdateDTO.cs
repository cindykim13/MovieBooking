using System.ComponentModel.DataAnnotations;

namespace MovieBookingAPI.Models.DTOs
{
    public class MovieUpdateDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? StoryLine { get; set; }

        [Range(1, 1000)]
        public int Duration { get; set; } // Thời lượng (phút)

        public int? ReleaseYear { get; set; }

        public string? AgeRating { get; set; } // P, C13, C18...

        public string? PosterUrl { get; set; }

        // Lưu ý: Nếu SP của bạn yêu cầu cập nhật cả Thể loại (Genres) hoặc Diễn viên (Casts),
        // bạn cần thêm List<int> GenreIds hoặc chuỗi JSON tương ứng tại đây.
    }
}