using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBookingAPI.DAO
{
    public class UserAuthData
    {
        [Column("userid")]
        public int UserId { get; set; }
        [Column("username")]
        public string Username { get; set; } = string.Empty;
        [Column("passwordhash")]
        public string PasswordHash { get; set; } = string.Empty;
        [Column("Role")]
        public string Role { get; set; } = string.Empty;
        [Column("fullname")]
        public string FullName { get; set; } = string.Empty;
    }
}