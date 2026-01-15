namespace MovieBooking.Domain.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public required List<string> Genres { get; set; }
        [Newtonsoft.Json.JsonIgnore] // Hoặc [System.Text.Json.Serialization.JsonIgnore]
        public string GenresDisplay => Genres != null ? string.Join(", ", Genres) : "";
        public int? GenreId { get; set; }
    }
}