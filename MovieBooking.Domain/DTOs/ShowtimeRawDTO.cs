namespace MovieBooking.Domain.DTOs
{
    // DTO hứng dữ liệu thô từ Database (Flat structure)
    public class ShowtimeRawDTO
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; } = string.Empty;
        public string CinemaAddress { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public int ShowtimeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal BasePrice { get; set; }
    }

    // DTO trả về Client: Thông tin suất chiếu
    public class ShowtimeDTO
    {
        public int ShowtimeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    // DTO trả về Client: Thông tin Rạp kèm danh sách suất
    public class CinemaShowtimeDTO
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<ShowtimeDTO> Showtimes { get; set; } = new List<ShowtimeDTO>();
    }
}