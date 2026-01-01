using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.DAO;
using MovieBookingAPI.Data;
using Npgsql; 
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public class AuthDAO : IAuthDAO
    {
        private readonly AppDbContext _context;

        public AuthDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> RegisterUserAsync(string username, string passwordHash, string email, string fullName, string phoneNumber)
        {
            // 1. Định nghĩa các tham số
            var parameters = new[]
            {
                new NpgsqlParameter("p_username", username),
                new NpgsqlParameter("p_passwordhash", passwordHash),
                new NpgsqlParameter("p_email", email),
                new NpgsqlParameter("p_fullname", (object)fullName ?? DBNull.Value),
                new NpgsqlParameter("p_phonenumber", (object)phoneNumber ?? DBNull.Value)
            };

            try
            {
                // 2. Gọi Function
                // Hàm usp_RegisterUser trong PostgreSQL trả về một bảng có một dòng, một cột (NewUserId)
                // Do đó, ta dùng SqlQueryRaw<int> để đọc giá trị đó
                var result = await _context.Database
                    .SqlQueryRaw<int>("SELECT * FROM usp_registeruser(@p_username, @p_passwordhash, @p_email, @p_fullname, @p_phonenumber)", parameters)
                    .ToListAsync();

                // 3. Trả về kết quả
                // Nếu thành công, result sẽ là một List<int> có 1 phần tử
                return result.FirstOrDefault();
            }
            catch (PostgresException ex)
            {
                // Xử lý các mã lỗi nghiệp vụ được RAISE từ PostgreSQL
                // 23505 là mã lỗi chuẩn cho Unique Violation
                if (ex.SqlState == "23505")
                {
                    if (ex.Message.Contains("Username")) return -1; // Mã lỗi tự quy ước: Trùng Username
                    if (ex.Message.Contains("Email")) return -2;    // Mã lỗi tự quy ước: Trùng Email
                }
                throw;
            }
        }

        public async Task<UserAuthData?> GetUserByUsernameAsync(string username)
        {
            var pUsername = new NpgsqlParameter("p_username", username);
            // 1. Đảm bảo tên function là chữ thường (PostgreSQL thường phân biệt)
            // 2. Sử dụng FromSqlRaw để ánh xạ kết quả bảng vào đối tượng UserAuthData
            var user = await _context.Set<UserAuthData>()
                .FromSqlRaw("SELECT * FROM usp_getuserbyusername(@p_username)", pUsername)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}