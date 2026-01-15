using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieBooking.Domain.DTOs
{
    public class CreateRoomRequestDTO
    {
        [Required]
        public int CinemaId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // --- CẬP NHẬT MỚI: Thêm 2 thuộc tính này để khớp với code giao diện (Image 2) ---
        public int NumberOfRows { get; set; } // Số hàng ghế (Ví dụ: 10)
        public int SeatsPerRow { get; set; }  // Số ghế mỗi hàng (Ví dụ: 12)

        // List này giữ lại để dùng nếu cần tùy chỉnh từng ghế, 
        // nhưng có thể khởi tạo rỗng nếu dùng logic tự sinh ghế.
        public List<SeatDefinitionDTO> Seats { get; set; } = new List<SeatDefinitionDTO>();
    }

    public class SeatDefinitionDTO
    {
        [Required]
        public int TypeId { get; set; }

        [Required]
        public string Row { get; set; } = string.Empty;

        [Required]
        public int Number { get; set; }

        [Required]
        public int GridRow { get; set; }

        [Required]
        public int GridColumn { get; set; }
    }
}