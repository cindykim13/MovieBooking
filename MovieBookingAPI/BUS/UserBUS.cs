using MovieBooking.Domain.DTOs;
using MovieBookingAPI.DAO;

namespace MovieBookingAPI.BUS
{
    public class UserBUS : IUserBUS
    {
        private readonly IUserDAO _userRepo;

        public UserBUS(IUserDAO userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserProfileDTO?> GetUserProfileAsync(int userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null) return null;

            // Mapping thủ công từ Entity sang DTO
            return new UserProfileDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            };
        }
        public async Task<bool> UpdateUserProfileAsync(int userId, UpdateProfileRequestDTO request)
        {
            // Gọi Repository để thực hiện cập nhật
            // Logic nghiệp vụ bổ sung (nếu cần): Kiểm tra email trùng lặp nếu SP chưa xử lý
            return await _userRepo.UpdateProfileAsync(userId, request.FullName, request.Email, request.PhoneNumber);
        }

        public async Task<(bool Success, string Message)> ChangePasswordAsync(int userId, ChangePasswordRequestDTO request)
        {
            // 1. Lấy thông tin user hiện tại từ DB để lấy Hash cũ
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null)
            {
                return (false, "Người dùng không tồn tại.");
            }
            // 2. Kiểm tra mật khẩu cũ (Verify)
            bool isOldPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);
            if (!isOldPasswordCorrect)
            {
                return (false, "Mật khẩu hiện tại không chính xác.");
            }
            // 3. Hash mật khẩu mới
            string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            // 4. Cập nhật xuống DB
            bool result = await _userRepo.ChangePasswordAsync(userId, newPasswordHash);
            if (result)
            {
                return (true, "Đổi mật khẩu thành công.");
            }
            return (false, "Đổi mật khẩu thất bại do lỗi hệ thống.");
        }
    }
}