namespace MovieBookingAPI.Domain.DTOs
{
    public class RoomDTO
    {
        public int RoomId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
    }
}