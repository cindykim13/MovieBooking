namespace MovieBookingAPI.Models.DTOs
{
    // DTO hứng dữ liệu thô từ Database (Flat structure)
    public class ShowtimeRawDto
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string CinemaAddress { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int ShowtimeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal BasePrice { get; set; }
    }

    // DTO trả về Client: Thông tin suất chiếu
    public class ShowtimeDto
    {
        public int ShowtimeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public decimal Price { get; set; }
    }

    // DTO trả về Client: Thông tin Rạp kèm danh sách suất
    public class CinemaShowtimeDto
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string Address { get; set; }
        public List<ShowtimeDto> Showtimes { get; set; } = new List<ShowtimeDto>();
    }
}