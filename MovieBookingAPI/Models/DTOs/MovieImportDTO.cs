namespace MovieBookingAPI.Models.DTOs
{
    public class MovieImportDTO
    {
        public string Title { get; set; } = string.Empty;
        public string StoryLine { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int Duration { get; set; }
        public int ReleaseYear { get; set; }
        public string AgeRating { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string PosterUrl { get; set; } = string.Empty;

        // Danh sách thể loại và diễn viên
        public List<string> Genres { get; set; } = new List<string>();
        public List<string> Casts { get; set; }  = new List<string>();
    }
}
