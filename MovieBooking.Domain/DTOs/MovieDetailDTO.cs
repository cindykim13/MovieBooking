namespace MovieBooking.Domain.DTOs
{
    public class MovieDetailDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string StoryLine { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int Duration { get; set; }
        public int ReleaseYear { get; set; }
        public string AgeRating { get; set; }  = string.Empty;
        public double Rating { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        // Trả về dạng mảng chuỗi để Frontend dễ render (VD: ["Action", "Sci-Fi"])
        public List<string> Genres { get; set; } = new List<string>();
        public List<string> Casts { get; set; } = new List<string>();
    }
}