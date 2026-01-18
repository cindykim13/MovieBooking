using System;
using System.ComponentModel.DataAnnotations;


namespace MovieBooking.Domain.DTOs
{
    public class UpdateShowtimeRequestDTO
    {
        [Required]
        public int MovieId { get; set; }


        [Required]
        public int RoomId { get; set; }

        [Required]
        public int CinemaId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }


        [Required]
        [Range(0, double.MaxValue)]
        public decimal BasePrice { get; set; }


        [Required]
        [Range(0, 2)] // 0: Closed, 1: Open, 2: Cancelled
        public int Status { get; set; }
    }
}
