using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MovieBooking.Domain.DTOs
{
    public class CreateRoomRequestDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn rạp chiếu.")]
        public int CinemaId { get; set; }

        [Required(ErrorMessage = "Tên phòng không được để trống.")]
        public string Name { get; set; } = string.Empty;

        // [QUAN TRỌNG]: Bắt buộc phải có ghế (lấy từ Template gửi lên)
        [Required(ErrorMessage = "Dữ liệu ghế bị thiếu.")]
        [MinLength(1, ErrorMessage = "Danh sách ghế không được trống. Vui lòng chọn Mẫu phòng.")]
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
