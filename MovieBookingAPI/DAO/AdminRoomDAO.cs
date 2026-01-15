using Microsoft.EntityFrameworkCore;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.Entities;
using MovieBooking.Domain.Entities;

namespace MovieBookingAPI.DAO
{
    public class AdminRoomDAO : IAdminRoomDAO
    {
        private readonly AppDbContext _context;

        public AdminRoomDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomDTO>> GetAllRoomsAsync()
        {
            return await _context.ScreenRooms
                .Include(r => r.CinemaId)
                .Select(r => new RoomDTO
                {
                    RoomId = r.Id,
                    RoomName = r.RoomName,
                    CinemaName = r.Cinema.Name,
                    TotalSeats = _context.Seats.Count(s => s.RoomId == r.Id && s.Status == 1),
                    Status = "Active"
                })
                .ToListAsync();
        }

        // SỬA LẠI LOGIC THÊM PHÒNG THEO DTO MỚI
        public async Task<bool> CreateRoomAsync(CreateRoomRequestDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Lưu Phòng
                var room = new ScreenRoom
                {
                    RoomName = dto.Name,
                    CinemaId = dto.CinemaId
                };
                _context.ScreenRooms.Add(room);
                await _context.SaveChangesAsync(); // Để lấy room.Id

                // 2. Lưu Danh Sách Ghế (Từ list client gửi lên)
                if (dto.Seats != null && dto.Seats.Any())
                {
                    var seats = dto.Seats.Select(s => new Seat
                    {
                        RoomId = room.Id,
                        SeatRow = s.Row,
                        SeatNumber = s.Number,
                        Type = s.TypeId,
                        Status = 1 // Mặc định Active
                        // Lưu ý: Nếu DB không có cột GridRow/GridColumn thì bỏ qua, 
                        // hoặc lưu vào cột JSON/Description nếu cần.
                    }).ToList();

                    _context.Seats.AddRange(seats);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.ScreenRooms.FindAsync(id);
            if (room == null) return false;

            var seats = _context.Seats.Where(s => s.RoomId == id);
            _context.Seats.RemoveRange(seats);
            _context.ScreenRooms.Remove(room);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}