using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBookingAPI.Models.Entities
{
    [Table("appuser")]
    public class AppUser
    {
        [Key]
        [Column("userid")]
        public int UserId { get; set; }
        [Column("username")]
        public string Username { get; set; } = string.Empty;
        [Column("passwordhash")]
        public string PasswordHash { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("fullname")]
        public string FullName { get; set; } = string.Empty;
        [Column("phonenumber")]
        public string? PhoneNumber { get; set; }
        [Column("Role")]
        public string Role { get; set; } = string.Empty;
        [Column("createdat")]
        public DateTime CreatedAt { get; set; }
    }
}