namespace MovieBookingAPI.Models.DTOs
{
    public class SeatDto
    {
        public int SeatId { get; set; }
        public string Row { get; set; }
        public int Number { get; set; }
        public int GridRow { get; set; }
        public int GridColumn { get; set; }
        public string SeatType { get; set; }
        public decimal Price { get; set; }

        // 0: Available, 1: Holding, 2: Sold
        public int Status { get; set; }
    }
}