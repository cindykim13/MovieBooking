using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using System.Data;

namespace MovieBookingAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> RegisterUserAsync(string username, string passwordHash, string email, string fullName, string phoneNumber)
        {
            var pUsername = new SqlParameter("@Username", username);
            var pPasswordHash = new SqlParameter("@PasswordHash", passwordHash);
            var pEmail = new SqlParameter("@Email", email);
            var pFullName = new SqlParameter("@FullName", fullName ?? (object)DBNull.Value);
            var pPhoneNumber = new SqlParameter("@PhoneNumber", phoneNumber ?? (object)DBNull.Value);
            var pReturnValue = new SqlParameter
            {
                ParameterName = "@ReturnValue",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue
            };
            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "usp_RegisterUser";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(new[] { pUsername, pPasswordHash, pEmail, pFullName, pPhoneNumber, pReturnValue });
                var result = await command.ExecuteScalarAsync();
                int returnCode = (int)pReturnValue.Value;
                if (returnCode < 0) return returnCode;

                if (result != null && int.TryParse(result.ToString(), out int newId))
                {
                    return newId;
                }
                return 0; 
            }
        }
        public async Task<UserAuthData> GetUserByUsernameAsync(string username)
        {
            UserAuthData user = null;

            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "usp_GetUserByUsername";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Username", username));

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new UserAuthData
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                            Role = reader.GetString(reader.GetOrdinal("Role")),
                            // Kiểm tra null cho các cột không bắt buộc nếu cần
                            FullName = !reader.IsDBNull(reader.GetOrdinal("FullName")) ? reader.GetString(reader.GetOrdinal("FullName")) : null
                        };
                    }
                }
            }
            return user;
        }
    }
}