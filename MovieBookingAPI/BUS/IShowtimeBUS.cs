using MovieBooking.Domain.DTOs;
using MovieBooking.Domain.Entities;
using MovieBookingAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingAPI.BUS
{
    public interface IShowtimeBUS
    {
        // [SỬA LẠI] Đổi Create -> Add
        Task<int> AddShowtimeAsync(int movieId, int roomId, DateTime startTime, decimal price);

        // [THÊM]
        Task UpdateShowtimeAsync(int id, int movieId, int roomId, DateTime startTime, decimal price);

        Task<IEnumerable<Seat>> GetSeatMapAsync(int showtimeId); // Sửa kiểu trả về thành Seat cho đơn giản

        Task<IEnumerable<Showtime>> GetShowtimesByMovieAsync(int movieId, DateTime date);
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date);
        Task DeleteShowtimeAsync(int id);
    }
}