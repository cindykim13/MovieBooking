namespace MovieBooking.Domain.DTOs
{
    public class CinemaDTO
    {
        public int CinemaId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}