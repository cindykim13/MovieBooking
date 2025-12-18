using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.Entities;

namespace MovieBookingAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByIdAsync(int userId)
        {
            // Sử dụng FromSqlRaw để thực thi câu lệnh SQL thuần
            // EF Core sẽ tự động ánh xạ kết quả vào Object AppUser
            var result = await _context.Users
                .FromSqlRaw("SELECT * FROM AppUser WHERE UserId = {0}", userId)
                .FirstOrDefaultAsync();

            return result;
        }
        public async Task<bool> UpdateProfileAsync(int userId, string fullName, string email, string phoneNumber)
        {
            // Định nghĩa các tham số SQL
            var pUserId = new SqlParameter("@UserId", userId);
            // Xử lý null cho các tham số tùy chọn (nếu có)
            var pFullName = new SqlParameter("@FullName", fullName ?? (object)DBNull.Value);
            var pEmail = new SqlParameter("@Email", email);
            var pPhoneNumber = new SqlParameter("@PhoneNumber", phoneNumber ?? (object)DBNull.Value);

            // SỬA ĐỔI QUAN TRỌNG:
            // Sử dụng SqlQueryRaw để đọc kết quả trả về từ câu lệnh SELECT @@ROWCOUNT trong SP.
            // ExecuteSqlRawAsync không đọc được SELECT khi có SET NOCOUNT ON.
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "EXEC usp_UpdateUserProfile @UserId, @FullName, @PhoneNumber, @Email",
                    pUserId, pFullName, pPhoneNumber, pEmail
                )
                .ToListAsync();

            // Lấy giá trị đầu tiên từ danh sách kết quả (chính là @@ROWCOUNT)
            int rowsAffected = result.FirstOrDefault();

            // Nếu số dòng > 0 nghĩa là cập nhật thành công
            return rowsAffected > 0;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string newPasswordHash)
        {
            var pUserId = new SqlParameter("@UserId", userId);
            var pNewHash = new SqlParameter("@NewPasswordHash", newPasswordHash);

            // Sử dụng SqlQueryRaw để lấy @@ROWCOUNT
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "EXEC usp_ChangeUserPassword @UserId, @NewPasswordHash",
                    pUserId, pNewHash
                )
                .ToListAsync();

            int rowsAffected = result.FirstOrDefault();
            return rowsAffected > 0;
        }
    }
}