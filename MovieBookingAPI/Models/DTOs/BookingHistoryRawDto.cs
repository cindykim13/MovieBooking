namespace MovieBookingAPI.Models.DTOs
{
    public class BookingHistoryRawDto
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public string PaymentMethod { get; set; }
        public string MovieTitle { get; set; }
        public string PosterUrl { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public string CinemaName { get; set; }
        public string RoomName { get; set; }
        public string SeatName { get; set; }
    }
}