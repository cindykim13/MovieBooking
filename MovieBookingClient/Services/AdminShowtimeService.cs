using MovieBooking.Domain.DTOs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class AdminShowtimeService : BaseApiService
    {
        public AdminShowtimeService() : base() { }

        // 1. LẤY DANH SÁCH LỊCH CHIẾU THEO NGÀY (READ)
        // Dùng để hiển thị trên DataGridView của Admin
        public async Task<List<ShowtimeAdminDTO>> GetShowtimesByDateAsync(DateTime date)
        {
            // API Endpoint này cần được tạo ở Backend
            var request = CreateRequest("/api/admin/showtimes", Method.Get);
            request.AddParameter("date", date.ToString("yyyy-MM-dd"));

            return await ExecuteAsync<List<ShowtimeAdminDTO>>(request);
        }


        // 2. LẤY THÔNG TIN CHI TIẾT MỘT LỊCH CHIẾU (READ ONE)
        // Dùng để đổ dữ liệu vào Form Sửa
        public async Task<ShowtimeAdminDTO> GetShowtimeByIdAsync(int id)
        {
            var request = CreateRequest($"/api/admin/showtimes/{id}", Method.Get);
            return await ExecuteAsync<ShowtimeAdminDTO>(request);
        }

        // 3. TẠO MỚI LỊCH CHIẾU (CREATE)
        public async Task<bool> CreateShowtimeAsync(CreateShowtimeRequestDTO dto)
        {
            var request = CreateRequest("/api/admin/showtimes", Method.Post);
            request.AddBody(dto);

            // API trả về 201 Created nếu thành công
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }

        // 4. CẬP NHẬT LỊCH CHIẾU (UPDATE)
        public async Task<bool> UpdateShowtimeAsync(int id, UpdateShowtimeRequestDTO dto)
        {
            var request = CreateRequest($"/api/admin/showtimes/{id}", Method.Put);
            request.AddBody(dto);

            // API trả về 200 OK nếu thành công
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }

        // 5. XÓA LỊCH CHIẾU (DELETE)
        public async Task<bool> DeleteShowtimeAsync(int id)
        {
            var request = CreateRequest($"/api/admin/showtimes/{id}", Method.Delete);

            // API trả về 204 No Content nếu thành công
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }
    }
}