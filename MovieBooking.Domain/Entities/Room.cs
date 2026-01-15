using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBookingClient.Domain.Entities
{
    // Ánh xạ bảng "Room" trong database
    [Table("Room")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int TotalSeats { get; set; }
        public string Status { get; set; }
        public int CinemaId { get; set; } // Khóa ngoại
    }
   
}