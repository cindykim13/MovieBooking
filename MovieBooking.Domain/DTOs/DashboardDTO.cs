using System.Collections.Generic;

namespace MovieBookingClient.Domain.DTOs
{
    // Cục tổng chứa tất cả thông tin
    public class DashboardDTO
    {
        public decimal TotalRevenue { get; set; } // Tổng doanh thu
        public int TotalTickets { get; set; }     // Tổng vé bán ra
        public int TotalMovies { get; set; }      // Số phim đang chiếu
        public List<TopMovieDTO> TopMovies { get; set; } // Danh sách top phim
    }

    // Cục nhỏ cho từng phim trong bảng xếp hạng
    public class TopMovieDTO
    {
        public int Rank { get; set; }
        public string MovieTitle { get; set; }
        public decimal Revenue { get; set; }
        public int TicketCount { get; set; }
    }
}