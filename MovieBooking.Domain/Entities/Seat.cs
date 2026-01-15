using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBookingAPI.Models.Entities // Đảm bảo namespace đúng với project của bạn
{
    [Table("seat")] // Tên bảng chữ thường
    public class Seat
    {
        [Key]
        [Column("seatid")] // Cột chữ thường
        public int SeatId { get; set; }

        // --- [SỬA QUAN TRỌNG] ---
        // 1. Đổi tên biến thành RoomId cho khớp với ScreenRoom.RoomId
        // 2. Map vào cột "roomid" (chữ thường)
        [Column("roomid")]
        public int RoomId { get; set; }

        [Column("seatrow")]
        public string SeatRow { get; set; } = string.Empty;

        [Column("seatnumber")]
        public int SeatNumber { get; set; }

        [Column("status")]
        public int Status { get; set; }
        public int Type { get; set; } // Hoặc string tùy logic của bạn
    }
}