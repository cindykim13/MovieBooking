using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Nếu Showtime.cs và ScreenRoom.cs cùng nằm trong MovieBooking.Domain.Entities 
// thì không cần using. Nếu khác, hãy thêm:
using MovieBooking.Domain.Entities;

namespace MovieBooking.Domain.Entities
{
    [Table("Showtime")]
    public class Showtime
    {
        [Key]
        public int ShowtimeId { get; set; } 

        [Required]
        public int MovieId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Status { get; set; } = 1;

        [ForeignKey("MovieId")]
        public virtual Movie? Movie { get; set; }

        [ForeignKey("RoomId")]
        public virtual ScreenRoom Room { get; set; } // Bắt buộc có dòng này để fix lỗi DAO
    }
}