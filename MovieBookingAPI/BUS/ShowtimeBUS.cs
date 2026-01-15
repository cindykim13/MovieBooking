using MovieBooking.Domain.DTOs;
using MovieBooking.Domain.Entities;
using MovieBookingAPI.DAO;
using MovieBookingAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public class ShowtimeBUS : IShowtimeBUS
    {
        private readonly IShowtimeDAO _showtimeDAO;

        public ShowtimeBUS(IShowtimeDAO showtimeDAO)
        {
            _showtimeDAO = showtimeDAO;
        }

        // [SỬA TÊN] Đổi từ CreateShowtimeAsync -> AddShowtimeAsync (để khớp với AdminController)
        public async Task<int> AddShowtimeAsync(int movieId, int roomId, DateTime startTime, decimal price)
        {
            return await _showtimeDAO.CreateShowtimeAsync(movieId, roomId, startTime, price);
        }

        // [THÊM HÀM] Thêm hàm Update (để khớp với AdminController)
        public async Task UpdateShowtimeAsync(int id, int movieId, int roomId, DateTime startTime, decimal price)
        {
            await _showtimeDAO.UpdateShowtimeAsync(id, movieId, roomId, startTime, price);
        }

        // Hàm lấy ghế (đã khớp với DAO vừa sửa ở bước 2)
        public async Task<IEnumerable<Seat>> GetSeatMapAsync(int showtimeId)
        {
            return await _showtimeDAO.GetSeatMapAsync(showtimeId);
        }

        // Các hàm cũ giữ nguyên
        public async Task<IEnumerable<Showtime>> GetShowtimesByMovieAsync(int movieId, DateTime date)
        {
            return await _showtimeDAO.GetShowtimesByMovieAsync(movieId, date);
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date)
        {
            return await _showtimeDAO.GetShowtimesByDateAsync(date);
        }

        public async Task DeleteShowtimeAsync(int id)
        {
            await _showtimeDAO.DeleteShowtimeAsync(id);
        }
    }
}