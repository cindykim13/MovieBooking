using System.ComponentModel.DataAnnotations;

namespace MovieBooking.Domain.DTOs
{
    public class PaymentRequestDTO
    {
        [Required(ErrorMessage = "Mã đơn hàng là bắt buộc.")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán.")]
        public string PaymentMethod { get; set; } = string.Empty; // Ví dụ: "Momo", "Visa"
    }
}