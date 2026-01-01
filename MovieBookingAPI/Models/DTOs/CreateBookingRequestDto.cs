using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieBookingAPI.Models.DTOs
{
    public class CreateBookingRequestDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn suất chiếu.")]
        public int ShowtimeId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ít nhất một ghế.")]
        [MinLength(1, ErrorMessage = "Danh sách ghế không được để trống.")]
        public List<int> SeatIds { get; set; } = new List<int>();
    }
}