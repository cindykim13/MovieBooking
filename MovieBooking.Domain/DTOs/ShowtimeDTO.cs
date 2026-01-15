using System;

namespace MovieBooking.Domain.DTOs // Giữ namespace này để khớp code cũ
{
    public class ShowtimeDTO
    {
        public int ShowtimeId { get; set; }

        // Tên biến phải khớp 100% với Server trả về
        public string MovieTitle { get; set; }
        public string RoomName { get; set; }
        public string CinemaName { get; set; }
        public string Status { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Price { get; set; }
    }
}