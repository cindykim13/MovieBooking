namespace MovieBooking.Domain.DTOs
{
    public class BookingHistoryRawDTO
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string MovieTitle { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public string CinemaName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public string SeatName { get; set; } = string.Empty;
    }
}