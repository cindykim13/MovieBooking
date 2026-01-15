using Microsoft.EntityFrameworkCore;
using MovieBooking.Domain.DTOs;
using MovieBooking.Domain.Entities;
using MovieBookingAPI.DAO;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.Entities;
using MovieBookingClient.Domain.Entities;

namespace MovieBookingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // --- 1. Khai báo các DbSet ---
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // DbSet cho các kết quả từ Stored Procedure (Keyless)
        public DbSet<UserAuthData> UserAuthData { get; set; }
        public DbSet<PagedMovieResult> PagedMovieResults { get; set; }
        public DbSet<MovieDetailRawResult> MovieDetailRawResults { get; set; }
        public DbSet<ShowtimeRawResult> ShowtimeRawResults { get; set; }
        public DbSet<BookingHistoryRawDTO> BookingHistoryRawResults { get; set; }
        public DbSet<SeatMapRawResult> SeatMapRawResults { get; set; }
        public DbSet<Movie> Movies { get; set; }

        // DbSet thực thể chính
        public DbSet<ScreenRoom> ScreenRooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Room> Rooms { get; set; }

        // --- 2. Cấu hình Fluent API ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ... (Các cấu hình Keyless giữ nguyên) ...
            modelBuilder.Entity<UserAuthData>().HasNoKey();
            modelBuilder.Entity<PagedMovieResult>().HasNoKey();
            modelBuilder.Entity<MovieDetailRawResult>().HasNoKey();
            modelBuilder.Entity<ShowtimeRawResult>().HasNoKey();
            modelBuilder.Entity<BookingHistoryRawDTO>().HasNoKey();
            modelBuilder.Entity<SeatMapRawResult>().HasNoKey();

            // --- 1. CẤU HÌNH SHOWTIME (Sửa cho PostgreSQL) ---
            modelBuilder.Entity<Showtime>(entity =>
            {
                entity.ToTable("showtime");
                entity.HasKey(e => e.ShowtimeId);

                // Map toàn bộ cột về chữ thường để tránh lỗi "column does not exist"
                entity.Property(e => e.ShowtimeId).HasColumnName("showtimeid");

                entity.Property(e => e.RoomId)
                      .HasColumnName("roomid")
                      .IsRequired();

                entity.Property(e => e.MovieId)
                      .HasColumnName("movieid")
                      .IsRequired();

                entity.Property(e => e.Price)
                      .HasColumnName("baseprice")
                      .HasColumnType("decimal(18, 2)");

                // [QUAN TRỌNG] Bổ sung các cột bị thiếu hôm trước
                entity.Property(e => e.StartTime).HasColumnName("starttime");
                entity.Property(e => e.EndTime).HasColumnName("endtime");
                entity.Property(e => e.Status).HasColumnName("status");

                // Cấu hình khóa ngoại
                entity.HasOne<ScreenRoom>()
                      .WithMany()
                      .HasForeignKey(d => d.RoomId)
                      .HasPrincipalKey(p => p.Id);
            });

            // --- 2. CẤU HÌNH SCREENROOM ---
            modelBuilder.Entity<ScreenRoom>(entity =>
            {
                entity.ToTable("screenroom");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("roomid");
                entity.Property(e => e.RoomName).HasColumnName("name");
                entity.Property(e => e.CinemaId).HasColumnName("cinemaid");
            });

            // --- 3. CẤU HÌNH SEAT ---
            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("seat");
                entity.HasKey(e => e.SeatId);

                entity.Property(e => e.SeatId).HasColumnName("seatid");
                entity.Property(e => e.RoomId).HasColumnName("roomid");
                entity.Property(e => e.SeatRow).HasColumnName("seatrow");
                entity.Property(e => e.SeatNumber).HasColumnName("seatnumber");
                entity.Property(e => e.Status).HasColumnName("status");
            });

            // --- 4. CẤU HÌNH CINEMA ---
            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.ToTable("cinema");
                entity.HasKey(e => e.CinemaId);

                entity.Property(e => e.CinemaId).HasColumnName("cinemaid");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Address).HasColumnName("address");
            });
        }
    }
}