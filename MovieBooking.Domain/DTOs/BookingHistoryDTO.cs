namespace MovieBooking.Domain.DTOs
{
    public class BookingHistoryDTO
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty; // Trả về chuỗi (Confirmed, Cancelled) thay vì số
        public string PaymentMethod { get; set; } = string.Empty;
        public string MovieTitle { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string CinemaName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public DateTime ShowTime { get; set; }

        // Danh sách ghế (VD: ["A1", "A2"])
        public List<string> Seats { get; set; } = new List<string>();
    }
}