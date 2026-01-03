using MovieBooking.Domain.DTOs;
using RestSharp;
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class AuthService : BaseApiService
    {
        public AuthService() : base() { }

        public async Task<LoginResponseDTO> LoginAsync(string username, string password)
        {
            var request = CreateRequest("/api/auth/login", Method.Post);

            var payload = new LoginRequestDTO
            {
                Username = username,
                Password = password
            };

            request.AddBody(payload);

            // Gọi ExecuteAsync từ lớp cha BaseApiService
            return await ExecuteAsync<LoginResponseDTO>(request);
        }
        public async Task<bool> RegisterAsync(RegisterRequestDTO registerData)
        {
            var request = CreateRequest("/api/auth/register", Method.Post);
            request.AddBody(registerData);

            // API trả về 201 Created nếu thành công, hoặc lỗi 400/409
            // BaseApiService.ExecuteAsync sẽ xử lý việc hiển thị lỗi nếu API trả về mã lỗi
            // Tuy nhiên, với RestSharp, ta cần kiểm tra Response cụ thể hơn nếu muốn custom logic

            try
            {
                var response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return true;
                }
                else
                {
                    // Gọi hàm xử lý lỗi chung của BaseService để hiển thị Message từ API (ví dụ: Trùng username)
                    // Lưu ý: Cần điều chỉnh access modifier của HandleError trong BaseApiService thành protected hoặc public nếu cần gọi từ đây, 
                    // hoặc để đơn giản, ta tái sử dụng cơ chế ExecuteAsync trả về object.

                    // Cách tiếp cận chuẩn:
                    // Vì API trả về cấu trúc { Message: "...", UserId: ... } khi thành công
                    // Ta có thể dùng ExecuteAsync<dynamic>
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // Cải tiến: Sử dụng phương thức ExecuteAsync<T> có sẵn ở BaseApiService
        public async Task<dynamic> RegisterAndGetDataAsync(RegisterRequestDTO registerData)
        {
            var request = CreateRequest("/api/auth/register", Method.Post);
            request.AddBody(registerData);
            return await ExecuteAsync<dynamic>(request);
        }
    }
}