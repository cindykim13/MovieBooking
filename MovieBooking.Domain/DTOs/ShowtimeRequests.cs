using System;
using System.ComponentModel.DataAnnotations;

namespace MovieBooking.Domain.DTOs
{
    // Class dùng để hứng dữ liệu khi TẠO MỚI
    public class CreateShowtimeRequest
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public decimal Price { get; set; }
    }

    // Class dùng để hứng dữ liệu khi CẬP NHẬT
    public class UpdateShowtimeRequest
    {
        [Required]
        public int ShowtimeId { get; set; } // Có ID để biết sửa dòng nào
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public decimal BasePrice { get; set; } // Lưu ý tên biến này phải khớp với Controller gọi
        public int Status { get; set; }
    }
}