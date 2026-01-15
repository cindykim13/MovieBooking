using System;

namespace MovieBookingAPI.DAO
{
    public class ShowtimeDetailRawResult
    {
        public int showtimeid { get; set; }
        public int movieid { get; set; }
        public string movietitle { get; set; } = string.Empty;
        public int cinemaid { get; set; }
        public string cinemaname { get; set; } = string.Empty;
        public int roomid { get; set; }
        public string roomname { get; set; } = string.Empty;
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public decimal baseprice { get; set; }
        public int status { get; set; }
    }
}