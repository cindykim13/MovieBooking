using System.Collections.Generic;
using System.Threading.Tasks;
using MovieBooking.Domain.DTOs; // Đảm bảo đã có DTO mới
using RestSharp;

namespace MovieBookingClient.Services
{
    public class RoomService : BaseApiService
    {
        public RoomService() : base() { }

        public async Task<List<RoomDTO>> GetAllRoomsAsync()
        {
            var request = new RestRequest("api/AdminRooms", Method.Get);
            return await ExecuteAsync<List<RoomDTO>>(request);
        }

        // CẬP NHẬT: Nhận CreateRoomRequestDTO
        public async Task<bool> CreateRoomAsync(CreateRoomRequestDTO dto)
        {
            var request = new RestRequest("api/AdminRooms", Method.Post);
            request.AddJsonBody(dto); // Tự động serialize cả danh sách ghế

            // ExecuteAsync trả về bool dựa trên IsSuccessful của response
            // Bạn có thể cần điều chỉnh BaseApiService nếu nó chưa hỗ trợ trả về bool trực tiếp
            // Hoặc dùng cách thủ công:
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var request = new RestRequest($"api/AdminRooms/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }
    }
}