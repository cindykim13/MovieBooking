namespace MovieBookingAPI.DAO
{
    public class ShowtimeRawResult
    {
        public int cinemaid { get; set; }
        public string cinemaname { get; set; } = string.Empty;
        public string cinemaaddress { get; set; } = string.Empty;
        public int roomid { get; set; }
        public string roomname { get; set; } = string.Empty;
        public int totalseats { get; set; }
        public int showtimeid { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public decimal baseprice { get; set; }
    }
}