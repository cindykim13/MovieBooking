namespace MovieBookingAPI.Models.DTOs
{
    public class SeatDTO
    {
        public int SeatId { get; set; }
        public string Row { get; set; } = string.Empty;
        public int Number { get; set; }
        public int GridRow { get; set; }
        public int GridColumn { get; set; }
        public string SeatType { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // 0: Available, 1: Holding, 2: Sold
        public int Status { get; set; }
    }
}