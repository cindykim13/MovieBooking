namespace MovieBookingAPI.DAO
{
    public class SeatMapRawResult
    {
        public int seatid { get; set; }
        public string Row { get; set; } = string.Empty; // "Row" và "Number" là từ khóa nên Function trả về viết hoa
        public int Number { get; set; }
        public int gridrow { get; set; }
        public int gridcolumn { get; set; }
        public string seattype { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int status { get; set; }
    }
}