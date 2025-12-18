using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieBookingAPI.Data;
using MovieBookingAPI.Models.DTOs;
using System.Data;

namespace MovieBookingAPI.Repositories
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly AppDbContext _context;

        public ShowtimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShowtimeRawDto>> GetRawShowtimesAsync(int movieId, DateTime date)
        {
            var rawList = new List<ShowtimeRawDto>();

            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "usp_GetShowtimesByMovie";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@MovieId", movieId));
                command.Parameters.Add(new SqlParameter("@ViewDate", date));

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        rawList.Add(new ShowtimeRawDto
                        {
                            CinemaId = reader.GetInt32(reader.GetOrdinal("CinemaId")),
                            CinemaName = reader.GetString(reader.GetOrdinal("CinemaName")),
                            CinemaAddress = reader.GetString(reader.GetOrdinal("CinemaAddress")),
                            RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),
                            RoomName = reader.GetString(reader.GetOrdinal("RoomName")),
                            ShowtimeId = reader.GetInt32(reader.GetOrdinal("ShowtimeId")),
                            StartTime = reader.GetDateTime(reader.GetOrdinal("StartTime")),
                            EndTime = reader.GetDateTime(reader.GetOrdinal("EndTime")),
                            BasePrice = reader.GetDecimal(reader.GetOrdinal("BasePrice"))
                        });
                    }
                }
            }
            return rawList;
        }

        public async Task<List<SeatDto>> GetSeatMapAsync(int showtimeId)
        {
            var seats = new List<SeatDto>();

            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "usp_GetShowtimeSeatMap";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ShowtimeId", showtimeId));

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        seats.Add(new SeatDto
                        {
                            SeatId = reader.GetInt32(reader.GetOrdinal("SeatId")),
                            Row = reader.GetString(reader.GetOrdinal("Row")),
                            Number = reader.GetInt32(reader.GetOrdinal("Number")),
                            GridRow = reader.GetInt32(reader.GetOrdinal("GridRow")),
                            GridColumn = reader.GetInt32(reader.GetOrdinal("GridColumn")),
                            SeatType = reader.GetString(reader.GetOrdinal("SeatType")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Status = reader.GetInt32(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }
            return seats;
        }
    }
}