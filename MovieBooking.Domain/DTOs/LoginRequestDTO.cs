using System.ComponentModel.DataAnnotations;

namespace MovieBooking.Domain.DTOs
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập hoặc email.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string Password { get; set; } = string.Empty;
    }
}