using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MovieBooking.Domain.DTOs
{
    public class CreateRoomRequestDTO
    {
        [Required]
        public int CinemaId { get; set; }


        [Required]
        public string Name { get; set; }  = string.Empty;


        [Required]
        [MinLength(1)]
        public List<SeatDefinitionDTO> Seats { get; set; } = new List<SeatDefinitionDTO>();
    }


    public class SeatDefinitionDTO
    {
        [Required]
        public int TypeId { get; set; }


        [Required]
        public string Row { get; set; } = string.Empty;


        [Required]
        public int Number { get; set; }


        [Required]
        public int GridRow { get; set; }


        [Required]
        public int GridColumn { get; set; }
    }
}
