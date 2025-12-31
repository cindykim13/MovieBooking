using System;
using System.ComponentModel.DataAnnotations;


namespace MovieBookingAPI.Models.DTOs
{
    public class UpdateShowtimeRequestDto
    {
        [Required]
        public int MovieId { get; set; }


        [Required]
        public int RoomId { get; set; }


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
