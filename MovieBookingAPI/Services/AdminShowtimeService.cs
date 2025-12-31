using Microsoft.Data.SqlClient;
using MovieBookingAPI.Models.DTOs;
using MovieBookingAPI.Repositories;
using System;
using System.Threading.Tasks;


namespace MovieBookingAPI.Services
{
    public class AdminShowtimeService : IAdminShowtimeService
    {
        private readonly IAdminShowtimeRepository _repo;


        public AdminShowtimeService(IAdminShowtimeRepository repo)
        {
            _repo = repo;
        }


        public async Task<int> CreateShowtimeAsync(CreateShowtimeRequestDto request)
        {
            try
            {
                return await _repo.CreateShowtimeAsync(request);
            }
            catch (SqlException ex)
            {
                // Bắt lỗi nghiệp vụ từ Stored Procedure
                if (ex.Number == 54004) // Lỗi trùng lịch
                {
                    throw new InvalidOperationException(ex.Message); // Message từ SP: "Phòng chiếu này đang bận..."
                }
                if (ex.Number == 54003) // Lỗi phim không tồn tại
                {
                    throw new ArgumentException(ex.Message);
                }
                if (ex.Number == 54002 || ex.Number == 54001) // Lỗi validation khác
                {
                    throw new ArgumentException(ex.Message);
                }


                throw; // Lỗi hệ thống khác
            }
        }
        public async Task UpdateShowtimeAsync(int showtimeId, UpdateShowtimeRequestDto request)
        {
            try
            {
                await _repo.UpdateShowtimeAsync(showtimeId, request);
            }
            catch (SqlException ex)
            {
                // Bắt lỗi nghiệp vụ từ Stored Procedure
                switch (ex.Number)
                {
                    case 54004: // Trùng lịch
                    case 54006: // Đã có vé bán
                        throw new InvalidOperationException(ex.Message);
                    case 54005: // Không tồn tại
                        throw new KeyNotFoundException(ex.Message);
                    default:
                        throw; // Lỗi hệ thống khác
                }
            }
        }
        public async Task DeleteShowtimeAsync(int showtimeId)
        {
            try
            {
                await _repo.DeleteShowtimeAsync(showtimeId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 54005) // Không tồn tại
                {
                    throw new KeyNotFoundException(ex.Message);
                }
                throw; // Lỗi khác
            }
        }

    }
}
