using System.ComponentModel.DataAnnotations;

namespace MovieBookingAPI.Models.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập hoặc email.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string Password { get; set; }
    }
}