    using System.ComponentModel.DataAnnotations;

    namespace MovieBooking.Domain.DTOs
    {
        // 1. DTO dùng để hiển thị danh sách phòng (Output)
        public class RoomDTO
        {
            public int RoomId { get; set; }
            public string RoomName { get; set; }
            public string CinemaName { get; set; } // Hiển thị tên rạp
            public int TotalSeats { get; set; }    // Tổng số ghế
            public string Status { get; set; }
        }
    }