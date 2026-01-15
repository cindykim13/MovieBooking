namespace MovieBooking.Domain.DTOs
{
    public class ShowtimeAdminDTO
    {
        public int ShowtimeId { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public int CinemaId { get; set; }
        public string MovieTitle { get; set; } = string.Empty; 
        public string RoomName { get; set; } = string.Empty;
        public string CinemaName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal BasePrice { get; set; }
        public int Status { get; set; }
    }
}