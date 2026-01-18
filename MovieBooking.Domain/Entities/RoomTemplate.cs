using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBookingAPI.Models.Entities
{
    [Table("roomtemplate")]
    public class RoomTemplate
    {
        [Key]
        [Column("templateid")]
        public int TemplateId { get; set; }

        [Column("templatename")]
        public string TemplateName { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("totalseats")]
        public int TotalSeats { get; set; }

        [Column("layoutjson", TypeName = "jsonb")] // Quan trọng: Khai báo kiểu jsonb
        public string LayoutJson { get; set; } = string.Empty;
    }
}