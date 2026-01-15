using MovieBookingClient.Session;
using RestSharp.Serializers.NewtonsoftJson;
using RestSharp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration; // Đảm bảo đã add reference System.Configuration

namespace MovieBookingClient.Services
{
    public abstract class BaseApiService
    {
        protected readonly RestClient _client;

        protected BaseApiService()
        {
            // Lấy URL từ file cấu hình (App.config hoặc AppSettings tĩnh)
            // Giả sử bạn lưu trong App.config là <add key="ApiBaseUrl" value="..."/>
            string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

            // Nếu bạn dùng một class tĩnh riêng để quản lý cấu hình, hãy thay bằng:
            // string baseUrl = MovieBookingClient.Settings.Config.BaseUrl;

            if (string.IsNullOrEmpty(baseUrl))
            {
                // Backup phòng trường hợp file config lỗi
                baseUrl = "https://localhost:7034";
            }

            var options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(30),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
        }

        // Hàm tạo Request chuẩn (Giữ nguyên logic đính kèm Token)
        protected RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);

            // Tự động đính kèm Token từ Session để mở "ổ khóa" 🔒 trên Swagger
            if (SessionManager.Instance.IsLoggedIn && !string.IsNullOrEmpty(SessionManager.Instance.AccessToken))
            {
                request.AddHeader("Authorization", $"Bearer {SessionManager.Instance.AccessToken}");
            }

            return request;
        }

        // Hàm thực thi Request (Dùng chung cho cả Get, Post, Put, Delete)
        protected async Task<T> ExecuteAsync<T>(RestRequest request)
        {
            try
            {
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
                MessageBox.Show("Phiên đăng nhập hết hạn hoặc chưa đăng nhập. Vui lòng thử lại.", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SessionManager.Instance.EndSession();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                MessageBox.Show("Tài khoản của bạn không có quyền Admin để thực hiện thao tác này.", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                errorMessage = errorMessage?.Replace("\"", "");
                MessageBox.Show($"Lỗi Server: {errorMessage}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}