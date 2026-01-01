using System;
using System.ComponentModel.DataAnnotations;


namespace MovieBookingAPI.Models.DTOs
{
    public class CreateShowtimeRequestDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn phim.")]
        public int MovieId { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn phòng chiếu.")]
        public int RoomId { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn thời gian bắt đầu.")]
        public DateTime StartTime { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập giá vé cơ bản.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá vé phải lớn hơn hoặc bằng 0.")]
        public decimal BasePrice { get; set; }
    }
}
