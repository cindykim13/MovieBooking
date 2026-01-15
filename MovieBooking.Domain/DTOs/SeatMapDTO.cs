using MovieBooking.Domain.DTOs;

public class SeatMapDTO
{
    public int TotalRows { get; set; }
    public int TotalColumns { get; set; }
    // Đây là danh sách mà bạn cần truy cập để dùng .Count
    public List<SeatDTO> Seats { get; set; }
}