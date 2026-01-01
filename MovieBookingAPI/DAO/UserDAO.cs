using Npgsql;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.Entities;

namespace MovieBookingAPI.DAO
{
    public class UserDAO : IUserDAO
    {
        private readonly AppDbContext _context;

        public UserDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetUserByIdAsync(int userId)
        {
            // Sửa lại cú pháp gọi Function cho PostgreSQL
            var p_userid = new NpgsqlParameter("p_userid", userId);

            // PostgreSQL không có SP trả về trực tiếp Entity, ta cần gọi Function trả về bảng và lấy dòng
            // Tạm thời, ta có thể dùng LINQ nếu SP phức tạp. Hoặc tạo SP riêng trả về 1 user
            // Để đơn giản, ta dùng LINQ cho hàm này
            return await _context.Set<AppUser>().FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<bool> UpdateProfileAsync(int userId, string fullName, string email, string phoneNumber)
        {
            var p_userid = new NpgsqlParameter("p_userid", userId);
            var p_fullname = new NpgsqlParameter("p_fullname", fullName ?? (object)DBNull.Value);
            var p_email = new NpgsqlParameter("p_email", email);
            var p_phonenumber = new NpgsqlParameter("p_phonenumber", phoneNumber ?? (object)DBNull.Value);

            // Sửa cú pháp gọi Function
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "SELECT usp_updateuserprofile(@p_userid, @p_fullname, @p_phonenumber, @p_email)",
                    p_userid, p_fullname, p_phonenumber, p_email
                )
                .ToListAsync();

            int rowsAffected = result.FirstOrDefault();
            return rowsAffected > 0;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string newPasswordHash)
        {
            var p_userid = new NpgsqlParameter("p_userid", userId);
            var p_newpasswordhash = new NpgsqlParameter("p_newpasswordhash", newPasswordHash);

            // Sửa cú pháp gọi Function
            var result = await _context.Database
                .SqlQueryRaw<int>(
                    "SELECT usp_changeuserpassword(@p_userid, @p_newpasswordhash)",
                    p_userid, p_newpasswordhash
                )
                .ToListAsync();

            int rowsAffected = result.FirstOrDefault();
            return rowsAffected > 0;
        }
    }
}