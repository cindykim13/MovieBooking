using MovieBookingClient.Session;
using RestSharp.Serializers.NewtonsoftJson;
using RestSharp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.Services
{
    public abstract class BaseApiService
    {
        protected readonly RestClient _client;

        // 👇 SỬA 1: Gán cứng URL Server của bạn vào đây cho chắc ăn
        private const string BASE_URL = "https://localhost:7034";

        protected BaseApiService()
        {
            var options = new RestClientOptions(BASE_URL)
            {
                Timeout = TimeSpan.FromSeconds(30), // 30s là quá đủ
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            // Cấu hình RestSharp dùng Newtonsoft.Json
            _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
        }

        // Hàm tạo Request chuẩn
        protected RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);

            // Tự động đính kèm Token nếu đã đăng nhập
            if (SessionManager.Instance.IsLoggedIn && !string.IsNullOrEmpty(SessionManager.Instance.AccessToken))
            {
                request.AddHeader("Authorization", $"Bearer {SessionManager.Instance.AccessToken}");
            }

            return request;
        }

        // Hàm thực thi Request
        protected async Task<T> ExecuteAsync<T>(RestRequest request)
        {
            try
            {
                // Gọi API
                var response = await _client.ExecuteAsync<T>(request);

                if (!response.IsSuccessful)
                {
                    HandleError(response);
                    return default;
                }

                return response.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối mạng: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default;
            }
        }

        // Xử lý lỗi tập trung
        private void HandleError(RestResponse response)
        {
            string errorMessage = !string.IsNullOrEmpty(response.Content) ? response.Content : response.StatusDescription;

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("Phiên đăng nhập hết hạn. Vui lòng đăng nhập lại.", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SessionManager.Instance.EndSession();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác này.", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Làm sạch thông báo lỗi (bỏ dấu ngoặc kép thừa nếu có)
                errorMessage = errorMessage.Replace("\"", "");
                MessageBox.Show($"Lỗi Server: {errorMessage}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}