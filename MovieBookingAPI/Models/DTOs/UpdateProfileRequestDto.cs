using System.ComponentModel.DataAnnotations;

namespace MovieBookingAPI.Models.DTOs
{
    public class UpdateProfileRequestDTO
    {
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        [StringLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }
    }
}