namespace MovieBookingAPI.Models.DTOs
{
    public class MovieDetailDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string StoryLine { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public int ReleaseYear { get; set; }
        public string AgeRating { get; set; }
        public double Rating { get; set; }
        public string PosterUrl { get; set; }
        public string Status { get; set; }

        // Trả về dạng mảng chuỗi để Frontend dễ render (VD: ["Action", "Sci-Fi"])
        public List<string> Genres { get; set; } = new List<string>();
        public List<string> Casts { get; set; } = new List<string>();
    }
}