using Npgsql;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.DTOs;
using System.Data;

namespace MovieBookingAPI.DAO
{
    public class ShowtimeDAO : IShowtimeDAO
    {
        private readonly AppDbContext _context;

        public ShowtimeDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShowtimeRawDTO>> GetRawShowtimesAsync(int movieId, DateTime date)
        {
            var p_movieid = new NpgsqlParameter("p_movieid", movieId);
            var p_viewdate = new NpgsqlParameter("p_viewdate", date);

            // Sử dụng FromSqlRaw
            var rawResult = await _context.Set<ShowtimeRawResult>()
                .FromSqlRaw("SELECT * FROM usp_getshowtimesbymovie(@p_movieid, @p_viewdate)",
                    p_movieid, p_viewdate)
                .ToListAsync();

            // Chuyển đổi từ lớp tạm (chữ thường) sang DTO (PascalCase)
            return rawResult.Select(r => new ShowtimeRawDTO
            {
                CinemaId = r.cinemaid,
                CinemaName = r.cinemaname,
                CinemaAddress = r.cinemaaddress,
                RoomId = r.roomid,
                RoomName = r.roomname,
                ShowtimeId = r.showtimeid,
                StartTime = r.starttime,
                EndTime = r.endtime,
                BasePrice = r.baseprice
            }).ToList();
        }
        public async Task<List<SeatDTO>> GetSeatMapAsync(int showtimeId)
        {
            // Định nghĩa tham số cho Function PostgreSQL (đúng tên, chữ thường)
            var p_showtimeid = new NpgsqlParameter("p_showtimeid", showtimeId);

            // Cần tạo lớp tạm để hứng kết quả thô có tên cột chữ thường
            // Tôi sẽ đặt tên là SeatMapRawResult để tránh nhầm lẫn
            var rawResult = await _context.Set<SeatMapRawResult>()
                .FromSqlRaw("SELECT * FROM usp_getshowtimeseatmap(@p_showtimeid)", p_showtimeid)
                .ToListAsync();

            // Chuyển đổi từ lớp tạm sang DTO chuẩn
            return rawResult.Select(r => new SeatDTO
            {
                SeatId = r.seatid,
                Row = r.Row,
                Number = r.Number,
                GridRow = r.gridrow,
                GridColumn = r.gridcolumn,
                SeatType = r.seattype,
                Price = r.price,
                Status = r.status
            }).ToList();
        }
    }
}