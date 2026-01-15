using System;

namespace MovieBooking.Domain.DTOs // Giữ namespace này nếu code cũ đang dùng
{
    public class ShowtimeDTO
    {
        public int ShowtimeId { get; set; }

        // Phải có các dòng này thì lưới mới hiện chữ được
        public string MovieTitle { get; set; }
        public string RoomName { get; set; }
        public string CinemaName { get; set; }
        public string Status { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Price { get; set; }
    }
}