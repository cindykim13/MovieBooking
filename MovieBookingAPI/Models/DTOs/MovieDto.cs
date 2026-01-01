namespace MovieBookingAPI.Models.DTOs
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Duration { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Genres { get; set; } = string.Empty; // Chuỗi thể loại gộp
    }
}