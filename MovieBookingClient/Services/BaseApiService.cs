using MovieBookingClient.Configs;
using MovieBookingClient.Session;
using RestSharp.Serializers.NewtonsoftJson; // Thêm using
using RestSharp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.Services
{
    public abstract class BaseApiService
    {
        protected readonly RestClient _client;

        protected BaseApiService()
        {
            var options = new RestClientOptions(AppSettings.BaseApiUrl)
            {
                Timeout = TimeSpan.FromSeconds(180),  // Tăng timeout lên 30s đề phòng mạng chậm
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson()); // Sử dụng Newtonsoft
        }

        // Hàm tạo Request chuẩn, tự động thêm Token nếu có
        protected RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);

            // Kiểm tra và đính kèm Token Bearer
            // [FIX CẢNH BÁO NULL]: Thêm toán tử ?. hoặc kiểm tra null an toàn
            if (SessionManager.Instance.IsLoggedIn && !string.IsNullOrEmpty(SessionManager.Instance.AccessToken))
            {
                request.AddHeader("Authorization", $"Bearer {SessionManager.Instance.AccessToken}");
            }

            return request;
        }

        // Hàm thực thi Request và Deserialize kết quả
        // [FIX LỖI CS8714]: Thêm ràng buộc 'where T : class' hoặc cho phép T nullable
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

        // Xử lý lỗi HTTP tập trung
        private void HandleError(RestResponse response)
        {
            // Ưu tiên lấy thông báo lỗi từ API, nếu không có thì lấy StatusDescription
            string errorMessage = !string.IsNullOrEmpty(response.Content) ? response.Content : response.StatusDescription;

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("Phiên đăng nhập hết hạn hoặc không hợp lệ. Vui lòng đăng nhập lại.", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SessionManager.Instance.EndSession();
                // Logic chuyển về màn hình đăng nhập sẽ được xử lý ở tầng UI
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác này.", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show($"Lỗi API ({response.StatusCode}): {errorMessage}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}