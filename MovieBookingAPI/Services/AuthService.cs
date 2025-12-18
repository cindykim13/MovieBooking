using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Repositories;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace MovieBookingAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepo, IConfiguration configuration)
        {
            _authRepo = authRepo;
            _configuration = configuration;
        }

        public async Task<(bool Success, string Message, int UserId)> RegisterAsync(RegisterRequestDto request)
        {
            // 1. Mã hóa mật khẩu (Hashing)
            // Sử dụng BCrypt để tạo chuỗi băm an toàn
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            // 2. Gọi Repository
            int result = await _authRepo.RegisterUserAsync(
                request.Username,
                passwordHash,
                request.Email,
                request.FullName,
                request.PhoneNumber
            );
            // 3. Xử lý kết quả trả về từ SP
            if (result == -1)
            {
                return (false, "Tên đăng nhập đã tồn tại.", 0);
            }

            if (result == -2)
            {
                return (false, "Email đã tồn tại.", 0);
            }

            if (result > 0)
            {
                return (true, "Đăng ký thành công.", result);
            }

            return (false, "Đăng ký thất bại do lỗi hệ thống.", 0);
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            // 1. Lấy thông tin user từ DB
            var user = await _authRepo.GetUserByUsernameAsync(request.Username);

            // 2. Kiểm tra tồn tại
            if (user == null)
            {
                return null; // Hoặc throw exception tùy phong cách xử lý lỗi
            }

            // 3. Xác thực mật khẩu (Verify Hash)
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                return null;
            }

            // 4. Sinh JWT Token
            string token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };
        }

        private string GenerateJwtToken(UserAuthData user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
                // Có thể thêm Claim FullName nếu cần
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}