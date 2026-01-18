using Microsoft.Data.SqlClient;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.BUS;
using MovieBookingAPI.DAO;
using MovieBookingAPI.Models.DTOs;
using Npgsql;
using System;
using System.Threading.Tasks;


namespace MovieBookingAPI.BUS
{
    public class AdminRoomBUS : IAdminRoomBUS
    {
        private readonly IAdminRoomDAO _repo;


        public AdminRoomBUS(IAdminRoomDAO repo)
        {
            _repo = repo;
        }

        public async Task<int> CreateRoomAsync(CreateRoomRequestDTO request)
        {
            // [LOGIC MỚI]: Validate bắt buộc phải có ghế (do Frontend lấy từ Template gửi lên)
            if (request.Seats == null || request.Seats.Count == 0)
            {
                throw new ArgumentException("Danh sách ghế không được trống. Vui lòng chọn một Mẫu phòng (Template) trước khi lưu.");
            }

            try
            {
                // Gọi Repository để lưu vào DB (Bulk Insert qua JSON)
                return await _repo.CreateRoomWithSeatsAsync(request);
            }
            catch (PostgresException ex) // Bắt PostgresException
            {
                // Bắt lỗi nghiệp vụ từ Function dựa trên Message (RAISE EXCEPTION)
                if (ex.Message.Contains("Tên phòng đã tồn tại"))
                {
                    throw new ArgumentException("Tên phòng đã tồn tại trong rạp này.");
                }
                if (ex.Message.Contains("Sơ đồ ghế không được trống"))
                {
                    throw new ArgumentException("Sơ đồ ghế không được để trống (Lỗi từ Database).");
                }

                // Lỗi Unique Key (trùng vị trí ghế Row + Number trong cùng 1 phòng)
                if (ex.SqlState == "23505")
                {
                    throw new ArgumentException("Sơ đồ ghế bị lỗi: Có vị trí ghế (Hàng/Số) bị trùng lặp.");
                }

                // Ném lỗi hệ thống nếu không phải các trường hợp trên
                throw new Exception("Lỗi cơ sở dữ liệu khi tạo phòng.", ex);
            }
        }

        public async Task DeleteRoomAsync(int roomId)
        {
            try
            {
                await _repo.DeleteRoomAsync(roomId);
            }
            catch (PostgresException ex)
            {
                if (ex.Message.Contains("Phòng chiếu không tồn tại"))
                {
                    throw new KeyNotFoundException($"Không tìm thấy phòng với ID = {roomId}");
                }
                if (ex.Message.Contains("Không thể xóa phòng"))
                {
                    throw new InvalidOperationException("Không thể xóa phòng này vì đã có dữ liệu lịch chiếu.");
                }

                throw new Exception("Lỗi cơ sở dữ liệu khi xóa phòng.", ex);
            }
        }
        public async Task<List<RoomTemplateDTO>> GetAllTemplatesAsync()
        {
            return await _repo.GetAllTemplatesAsync();
        }
    }
}
