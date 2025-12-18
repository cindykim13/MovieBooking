using System.ComponentModel.DataAnnotations;

namespace MovieBookingAPI.Models.DTOs
{
    public class PaymentRequestDto
    {
        [Required(ErrorMessage = "Mã đơn hàng là bắt buộc.")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán.")]
        public string PaymentMethod { get; set; } // Ví dụ: "Momo", "Visa"
    }
}