using Microsoft.Data.SqlClient;
using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Repositories;
using System;
using System.Threading.Tasks;


namespace MovieBookingAPI.Services
{
    public class AdminRoomService : IAdminRoomService
    {
        private readonly IAdminRoomRepository _repo;


        public AdminRoomService(IAdminRoomRepository repo)
        {
            _repo = repo;
        }


        public async Task<int> CreateRoomAsync(CreateRoomRequestDto request)
        {
            // Có thể thêm logic validate business tại đây
            // VD: Kiểm tra số lượng ghế không vượt quá giới hạn...
            try
            {
                return await _repo.CreateRoomWithSeatsAsync(request);
            }
            catch (SqlException ex)
            {
                // Bắt lỗi nghiệp vụ từ SP
                if (ex.Number >= 55001 && ex.Number <= 55003)
                {
                    throw new ArgumentException(ex.Message);
                }
                throw;
            }
        }
        public async Task DeleteRoomAsync(int roomId)
        {
            try
            {
                await _repo.DeleteRoomAsync(roomId);
            }
            catch (SqlException ex)
            {
                // Lỗi nghiệp vụ từ SP
                if (ex.Number == 55004) // Không tồn tại
                {
                    throw new KeyNotFoundException(ex.Message);
                }
                if (ex.Number == 55005) // Đã có lịch chiếu
                {
                    throw new InvalidOperationException(ex.Message);
                }
                throw; // Lỗi hệ thống khác
            }
        }

    }
}
