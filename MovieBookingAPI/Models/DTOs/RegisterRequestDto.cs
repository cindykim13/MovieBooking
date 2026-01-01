using System.ComponentModel.DataAnnotations;

namespace MovieBookingAPI.Models.DTOs
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập từ 3 đến 50 ký tự.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}