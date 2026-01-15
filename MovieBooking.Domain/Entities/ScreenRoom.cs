using MovieBookingAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// SỬA: Đưa về Namespace Domain để Showtime.cs có thể gọi được
namespace MovieBooking.Domain.Entities
{
    [Table("screenroom")] // Tên bảng trong DB
    public class ScreenRoom
    {
        [Key]
        [Column("RoomId")] // Tên cột trong DB
        public int Id { get; set; }

        [Column("Name")]
        public string RoomName { get; set; } = string.Empty;

        [Column("CinemaId")]
        public int CinemaId { get; set; }

        // Object liên kết
        [ForeignKey("CinemaId")]
        public virtual Cinema Cinema { get; set; }
    }
}