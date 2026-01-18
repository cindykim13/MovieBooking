using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.DAO;
using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Models.Entities;

namespace MovieBookingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Khai báo các DbSet
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Genre> Genres { get; set; } 

        // DbSet cho các kết quả từ SP
        public DbSet<UserAuthData> UserAuthData { get; set; }
        public DbSet<PagedMovieResult> PagedMovieResults { get; set; }
        public DbSet<MovieDetailRawResult> MovieDetailRawResults { get; set; }
        public DbSet<ShowtimeRawResult> ShowtimeRawResults { get; set; }
        public DbSet<BookingHistoryRawDTO> BookingHistoryRawResults { get; set; }
        public DbSet<CinemaDTO> CinemaResults { get; set; }
        public DbSet<ScreenRoomRawResult> ScreenRoomRawResults { get; set; }
        public DbSet<ShowtimeDetailRawResult> ShowtimeDetailRawResults { get; set; }
        public DbSet<AdminShowtimeRawResult> AdminShowtimeRawResults { get; set; }
        public DbSet<RoomTemplate> RoomTemplates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình ánh xạ bảng
            modelBuilder.Entity<AppUser>().ToTable("appuser");
            modelBuilder.Entity<Genre>().ToTable("genre"); 

            // Cấu hình các kiểu không có khóa
            modelBuilder.Entity<UserAuthData>().HasNoKey();
            modelBuilder.Entity<PagedMovieResult>().HasNoKey();
            modelBuilder.Entity<MovieDetailRawResult>().HasNoKey();
            modelBuilder.Entity<ShowtimeRawResult>().HasNoKey();
            modelBuilder.Entity<SeatMapRawResult>().HasNoKey();
            modelBuilder.Entity<BookingHistoryRawDTO>().HasNoKey();
            modelBuilder.Entity<CinemaDTO>().HasNoKey();
            modelBuilder.Entity<ScreenRoomRawResult>().HasNoKey();
            modelBuilder.Entity<ShowtimeDetailRawResult>().HasNoKey();
            modelBuilder.Entity<AdminShowtimeRawResult>().HasNoKey();
            modelBuilder.Entity<RoomTemplate>().ToTable("roomtemplate");
        }
    }
}