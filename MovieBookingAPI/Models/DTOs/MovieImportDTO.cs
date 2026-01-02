using Newtonsoft.Json; 
using System.Collections.Generic;

namespace MovieBookingAPI.Models.DTOs
{
    public class MovieImportDTO
    {
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("storyLine")]
        public string StoryLine { get; set; } = string.Empty;

        [JsonProperty("director")]
        public string Director { get; set; } = string.Empty;

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("releaseYear")]
        public int ReleaseYear { get; set; }

        [JsonProperty("ageRating")]
        public string AgeRating { get; set; } = string.Empty;

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("posterUrl")]
        public string PosterUrl { get; set; } = string.Empty;

        [JsonProperty("genres")]
        public List<string> Genres { get; set; } = new List<string>();

        [JsonProperty("casts")]
        public List<string> Casts { get; set; } = new List<string>();
    }
}