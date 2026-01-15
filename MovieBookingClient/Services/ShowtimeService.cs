using MovieBooking.Domain.DTOs;
using MovieBookingClient.Configs;
using MovieBookingClient.Global;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class ShowtimeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ShowtimeService()
        {
            // Bỏ qua lỗi SSL (nếu chạy localhost bị lỗi bảo mật)
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            _httpClient = new HttpClient(handler);
            _apiBaseUrl = $"{AppSettings.BaseApiUrl}/api";
        }

        private void AttachAuthToken()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            if (UserSession.IsLoggedIn)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", UserSession.AccessToken);
            }
        }

        // 1. Lấy danh sách (Giữ nguyên)
        public async Task<List<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date)
        {
            AttachAuthToken();
            string url = $"{_apiBaseUrl}/showtimes?date={date:yyyy-MM-dd}";
            return await GetListAsync<ShowtimeDTO>(url);
        }

        // 2. Thêm lịch chiếu (SỬA: Trả về bool)
        public async Task<bool> CreateShowtimeAsync(CreateShowtimeRequest request)
        {
            try
            {
                AttachAuthToken();
                // Dùng đường dẫn tuyệt đối, bỏ hết các hàm bổ trợ phức tạp
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                // Gửi POST chuẩn đến /api/admin/showtimes
                var response = await _httpClient.PostAsync($"{_apiBaseUrl.TrimEnd('/')}/admin/showtimes", content);

                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // 3. Sửa lịch chiếu (SỬA: Trả về bool)
        // Lưu ý: Tùy Backend của bạn có DTO Update riêng hay dùng chung Create
        public async Task<bool> UpdateShowtimeAsync(int id, CreateShowtimeRequest request)
        {
            AttachAuthToken();
            string url = $"{_apiBaseUrl}/admin/showtimes/{id}";
            return await PutAsync(url, request);
        }

        // 4. Xóa lịch chiếu (SỬA: Trả về bool)
        public async Task<bool> DeleteShowtimeAsync(int id)
        {
            try
            {
                AttachAuthToken();
                string url = $"{_apiBaseUrl}/admin/showtimes/{id}";
                var response = await _httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // 5. Khách hàng xem lịch theo phim
        public async Task<List<ShowtimeDTO>> GetShowtimesByMovieAsync(int movieId, DateTime? date = null)
        {
            var queryDate = date ?? DateTime.Now;
            string url = $"{_apiBaseUrl}/showtimes?movieId={movieId}&date={queryDate:yyyy-MM-dd}";
            return await GetListAsync<ShowtimeDTO>(url);
        }

        // --- CÁC HÀM HELPER GENERIC ---

        private async Task<List<T>> GetListAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return new List<T>();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch
            {
                return new List<T>(); // Trả về list rỗng thay vì null để tránh lỗi UI
            }
        }

        // SỬA: Hàm Post trả về bool (True nếu thành công, False nếu thất bại)
        private async Task<bool> PostAsync(string url, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                // Nếu thành công (200-299) -> True
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // SỬA: Hàm Put trả về bool
        private async Task<bool> PutAsync(string url, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(url, content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        // Fix lỗi CS1061
        public async Task<SeatMapDTO> GetSeatMapAsync(int showtimeId)
        {
            return await _httpClient.GetFromJsonAsync<SeatMapDTO>($"api/admin/showtimes/{showtimeId}/seats");
        }
    }
}