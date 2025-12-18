namespace MovieBookingAPI.Models.DTOs
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public string PosterUrl { get; set; }
        public string Status { get; set; }
        public string Genres { get; set; } // Chuỗi thể loại gộp
    }
}