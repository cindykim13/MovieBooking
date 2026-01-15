using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBookingAPI.Models.Entities
{
    [Table("cinema")] // Tên bảng trong Postgres (thường là chữ thường)
    public class Cinema
    {
        [Key]
        [Column("cinemaid")]
        public int CinemaId { get; set; }

        [Column("name")] // Cột chứa tên rạp
        public string Name { get; set; } = string.Empty;

        [Column("address")]
        public string? Address { get; set; }
    }
}