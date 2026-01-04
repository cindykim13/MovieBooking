using System;

namespace MovieBooking.Domain.DTOs
{
    public class BookingContextDTO
    {
        public int ShowtimeId { get; set; } 
        public string MovieTitle { get; set; } = string.Empty;
        public string CinemaName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public DateTime ShowTime { get; set; }
        public string PosterUrl { get; set; } = string.Empty;   // [QUAN TRỌNG] Thêm trường này
    }
}