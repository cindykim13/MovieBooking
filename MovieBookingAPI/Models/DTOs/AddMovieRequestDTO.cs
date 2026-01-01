using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MovieBookingAPI.Models.DTOs
{
    public class AddMovieRequestDTO
    {
        [Required(ErrorMessage = "Tên phim không được để trống.")]
        public string Title { get; set; } = string.Empty;


        public string StoryLine { get; set; } = string.Empty;


        public string Director { get; set; } = string.Empty;


        [Range(1, 1000, ErrorMessage = "Thời lượng phải lớn hơn 0.")]
        public int Duration { get; set; }


        [Range(1900, 2100, ErrorMessage = "Năm phát hành không hợp lệ.")]
        public int ReleaseYear { get; set; }


        public string AgeRating { get; set; } = string.Empty;


        [Range(0, 10, ErrorMessage = "Điểm đánh giá phải từ 0 đến 10.")]
        public double Rating { get; set; }


        public string PosterUrl { get; set; } = string.Empty;


        public string Status { get; set; } = string.Empty; // Now Showing, Coming Soon, Ended


        // Danh sách tên thể loại (VD: ["Hành động", "Hài"])
        public List<string> Genres { get; set; } = new List<string>();


        // Danh sách tên diễn viên
        public List<string> Casts { get; set; } = new List<string>();
    }
}

