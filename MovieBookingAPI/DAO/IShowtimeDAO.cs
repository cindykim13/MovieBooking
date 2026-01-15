using MovieBooking.Domain.DTOs;
using MovieBooking.Domain.Entities;
using MovieBookingAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.DAO
{
    public interface IShowtimeDAO
    {
        // Các hàm cũ
        Task<int> CreateShowtimeAsync(int movieId, int roomId, DateTime startTime, decimal price);
        Task UpdateShowtimeAsync(int id, int movieId, int roomId, DateTime startTime, decimal price);
        Task DeleteShowtimeAsync(int id);
        Task<Showtime?> GetShowtimeByIdAsync(int id);
        Task<IEnumerable<Showtime>> GetShowtimesByMovieAsync(int movieId, DateTime date);
        Task<Showtime?> GetShowtimeDetailAsync(int showtimeId);
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date);

        // [MỚI - SỬA LỖI] Thêm hàm lấy ghế
        // Tạm thời trả về dynamic hoặc Seat entity tùy bạn, ở đây mình trả về List<Seat> cho đơn giản
        Task<IEnumerable<Seat>> GetSeatMapAsync(int showtimeId);
    }
}