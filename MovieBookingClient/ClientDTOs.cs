using System;
using System.Collections.Generic;

// 👇 Đặt Namespace khác hẳn so với code chung để tránh nhầm lẫn
namespace MovieBookingClient.LocalDTOs
{
    // 1. Tạo lại ShowtimeDTO phiên bản Client (Có thêm CinemaName, Address, Showtimes...)
    //    để phục vụ giao diện UC_SelectShowtime của bạn
    public class ShowtimeDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = "";
        public int RoomId { get; set; }
        public string RoomName { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }

        // Các thuộc tính UI của bạn đang cần
        public string CinemaName { get; set; } = "Galaxy Cinema";
        public string Address { get; set; } = "TP.HCM";
        public string TimeDisplay => $"{StartTime:HH:mm} - {EndTime:HH:mm}";

        // List lồng nhau để phục vụ vòng lặp cũ của bạn
        public List<ShowtimeDTO> Showtimes { get; set; } = new List<ShowtimeDTO>();
    }

    // 2. Class Ghế
    public class SeatDTO
    {
        public int Id { get; set; }
        public string Row { get; set; } = "A";
        public int Number { get; set; } = 1;
        public string Status { get; set; } = "Available";
        public decimal Price { get; set; }
    }

    // 3. Class Ngữ cảnh đặt vé
    public class BookingContextDTO
    {
        public int ShowtimeId { get; set; }
        public string MovieTitle { get; set; } = "";
        public string CinemaName { get; set; } = "";
        public string RoomName { get; set; } = "";
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
    }

    // 4. Class Gửi yêu cầu đặt vé
    public class CreateBookingRequestDTO
    {
        public int ShowtimeId { get; set; }
        public List<int> SeatIds { get; set; } = new List<int>();
        public int UserId { get; set; }
    }
}