namespace MovieBookingAPI.Models.DTOs
{
    public class BookingHistoryDto
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Trả về chuỗi (Confirmed, Cancelled) thay vì số
        public string PaymentMethod { get; set; }
        public string MovieTitle { get; set; }
        public string PosterUrl { get; set; }
        public string CinemaName { get; set; }
        public string RoomName { get; set; }
        public DateTime ShowTime { get; set; }

        // Danh sách ghế (VD: ["A1", "A2"])
        public List<string> Seats { get; set; } = new List<string>();
    }
}