using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Domain.DTOs;
using MovieBookingAPI.Models.DTOs;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieBookingClient.Services
{
    public class AdminRoomService : BaseApiService
    {
        public AdminRoomService() : base() { }

        // Gọi API lấy danh sách Mẫu phòng
        public async Task<List<RoomTemplateDTO>> GetRoomTemplatesAsync()
        {
            var request = CreateRequest("/api/admin/rooms/templates", Method.Get);
            return await ExecuteAsync<List<RoomTemplateDTO>>(request);
        }

        // Gọi API lấy danh sách các Phòng đã tạo
        public async Task<List<RoomDTO>> GetRoomsAsync(int? cinemaId = null)
        {
            // [SỬA LỖI TẠI ĐÂY]
            // Sửa lại route cho đúng Endpoint đã có
            var request = CreateRequest("/api/Cinemas/rooms", Method.Get);

            if (cinemaId.HasValue && cinemaId > 0)
            {
                request.AddParameter("cinemaId", cinemaId.Value);
            }

            return await ExecuteAsync<List<RoomDTO>>(request);
        }

        // Gọi API để tạo một Phòng mới
        public async Task<int> CreateRoomAsync(CreateRoomRequestDTO requestDto)
        {
            var request = CreateRequest("/api/admin/rooms", Method.Post);
            request.AddBody(requestDto);

            // API trả về { message, roomId }, dùng dynamic để lấy roomId
            var result = await ExecuteAsync<dynamic>(request);
            if (result != null && result.roomId != null)
            {
                return (int)result.roomId;
            }
            return 0;
        }
        // Gọi API để Xóa phòng
        public async Task<bool> DeleteRoomAsync(int roomId)
        {
            // Tạo request đến API xóa phòng với ID tương ứng
            var request = CreateRequest($"/api/admin/rooms/{roomId}", Method.Delete);

            // Thực thi request. BaseApiService sẽ tự động ném ra Exception nếu có lỗi (404, 409...)
            // Nếu không có lỗi, coi như thành công.
            // Chúng ta không cần hứng kết quả cụ thể nếu API DELETE trả về 204 No Content.
            var response = await _client.ExecuteAsync(request);

            // Trả về true nếu thành công (mã 2xx)
            return response.IsSuccessful;
        }
    }
}