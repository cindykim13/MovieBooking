using Microsoft.Data.SqlClient;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.DAO;
using MovieBookingAPI.Domain.DTOs;
using Npgsql;
using System;
using System.Collections.Generic; // Thêm cho KeyNotFoundException
using System.Threading.Tasks;


namespace MovieBookingAPI.BUS
{
    public class AdminShowtimeBUS : IAdminShowtimeBUS
    {
        private readonly IAdminShowtimeDAO _repo;


        public AdminShowtimeBUS(IAdminShowtimeDAO repo)
        {
            _repo = repo;
        }


        public async Task<int> CreateShowtimeAsync(CreateShowtimeRequestDTO request)
        {
            // 1. Validate Business Logic: Phòng có thuộc Rạp không?
            bool isValidRoom = await _repo.IsRoomInCinemaAsync(request.RoomId, request.CinemaId);
            if (!isValidRoom)
            {
                throw new ArgumentException($"Phòng chiếu {request.RoomId} không thuộc Rạp {request.CinemaId}.");
            }

            try
            {
                return await _repo.CreateShowtimeAsync(request);
            }
            catch (PostgresException ex) // Sửa thành PostgresException
            {
                // Bắt lỗi nghiệp vụ từ Function
                if (ex.SqlState == "P0004") // Trùng lịch
                {
                    throw new InvalidOperationException("Phòng chiếu bị trùng lịch.");
                }
                if (ex.SqlState == "P0003") // Phim không tồn tại
                {
                    throw new ArgumentException("Phim không tồn tại.");
                }
                if (ex.SqlState == "P0002") // Lỗi thời gian
                {
                    throw new ArgumentException("Thời gian chiếu phải ở tương lai.");
                }

                throw; // Lỗi hệ thống khác
            }
        }

        public async Task UpdateShowtimeAsync(int showtimeId, UpdateShowtimeRequestDTO request)
        {
            try
            {
                await _repo.UpdateShowtimeAsync(showtimeId, request);
            }
            catch (PostgresException ex)
            {
                if (ex.Message.Contains("bị trùng lịch"))
                {
                    throw new InvalidOperationException("Phòng chiếu bị trùng lịch với suất chiếu khác.");
                }
                if (ex.Message.Contains("đã có vé bán") || ex.Message.Contains("đã có khách đặt"))
                {
                    throw new InvalidOperationException("Không thể sửa thông tin quan trọng vì đã có khách đặt vé.");
                }
                if (ex.Message.Contains("Suất chiếu không tồn tại"))
                {
                    throw new KeyNotFoundException($"Không tìm thấy suất chiếu ID = {showtimeId}");
                }
                throw new Exception("Lỗi cơ sở dữ liệu khi cập nhật lịch chiếu.", ex);
            }
        }

        public async Task<string> DeleteShowtimeAsync(int showtimeId)
        {
            try
            {
                // Gọi DAO và nhận kết quả (Soft Deleted / Hard Deleted)
                return await _repo.DeleteShowtimeAsync(showtimeId);
            }
            catch (PostgresException ex)
            {
                // Bắt lỗi nghiệp vụ: Suất chiếu không tồn tại (đã định nghĩa trong Function)
                if (ex.Message.Contains("Suất chiếu không tồn tại"))
                {
                    throw new KeyNotFoundException($"Không tìm thấy suất chiếu với ID = {showtimeId}");
                }

                // Ném các lỗi khác (ví dụ lỗi kết nối)
                throw new Exception("Lỗi cơ sở dữ liệu khi xóa lịch chiếu.", ex);
            }
        }
        public async Task<ShowtimeDetailDTO?> GetShowtimeDetailAsync(int showtimeId)
        {
            return await _repo.GetShowtimeDetailAsync(showtimeId);
        }
        public async Task<List<ShowtimeAdminDTO>> GetShowtimesByDateAsync(DateTime date)
        {
            return await _repo.GetShowtimesByDateAsync(date);
        }
    }
}
