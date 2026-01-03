using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBookingAPI.Models.Entities
{
    [Table("genre")]
    public class Genre
    {
        [Key]
        [Column("genreid")] // Ánh xạ tới cột "genreid"
        public int GenreId { get; set; }

        [Column("genrename")] // Ánh xạ tới cột "genrename"
        public string GenreName { get; set; } = string.Empty;
    }
}