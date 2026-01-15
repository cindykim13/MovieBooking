using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking.Domain.DTOs
{
   public class CreateBookingRequestDTO
    {
        public int Id { get; set; }
        public int? SeatIds { get; set; }
        public int? ShowtimeId { get; set; }
    }
}
