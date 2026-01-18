using MovieBooking.Domain.DTOs;
using System.Collections.Generic;

namespace MovieBookingAPI.Models.DTOs
{
    public class RoomTemplateDTO
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        // Trả về danh sách ghế đã parse sẵn để Frontend dễ dùng
        public List<SeatDefinitionDTO> Seats { get; set; } = new List<SeatDefinitionDTO>();
    }
}