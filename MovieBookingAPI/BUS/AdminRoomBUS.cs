using Microsoft.Data.SqlClient;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.BUS;
using MovieBookingAPI.DAO;
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
            try
            {
                return await _repo.CreateRoomWithSeatsAsync(request);
            }
            catch (PostgresException ex) // Bắt PostgresException
            {
                // Bắt lỗi nghiệp vụ từ Function dựa trên Message
                if (ex.Message.Contains("Tên phòng đã tồn tại"))
                {
                    throw new ArgumentException("Tên phòng đã tồn tại trong rạp này.");
                }
                if (ex.Message.Contains("Sơ đồ ghế không được trống"))
                {
                    throw new ArgumentException("Sơ đồ ghế không được để trống.");
                }
                // Lỗi Unique Key (trùng vị trí ghế)
                if (ex.SqlState == "23505")
                {
                    throw new ArgumentException("Sơ đồ ghế bị lỗi: Có vị trí ghế (Hàng/Số) bị trùng lặp.");
                }

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
    }
}
