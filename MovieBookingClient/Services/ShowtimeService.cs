using MovieBooking.Domain.DTOs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class ShowtimeService : BaseApiService
    {
        public async Task<List<CinemaShowtimeDTO>> GetShowtimesByMovieAsync(int movieId, DateTime date)
        {
            var request = CreateRequest("/api/showtimes", Method.Get);
            request.AddParameter("movieId", movieId);
            // Định dạng ngày yyyy-MM-dd để gửi lên API chuẩn xác
            request.AddParameter("date", date.ToString("yyyy-MM-dd"));

            return await ExecuteAsync<List<CinemaShowtimeDTO>>(request);
        }
        public async Task<List<SeatDTO>> GetSeatMapAsync(int showtimeId)
        {
            var request = CreateRequest($"/api/showtimes/{showtimeId}/seats", Method.Get);
            return await ExecuteAsync<List<SeatDTO>>(request);
        }
    }
}