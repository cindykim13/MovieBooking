using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MovieBookingAPI.Models.DTOs
{
    public class CreateRoomRequestDto
    {
        [Required]
        public int CinemaId { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        [MinLength(1)]
        public List<SeatDefinitionDto> Seats { get; set; }
    }


    public class SeatDefinitionDto
    {
        [Required]
        public int TypeId { get; set; }


        [Required]
        public string Row { get; set; }


        [Required]
        public int Number { get; set; }


        [Required]
        public int GridRow { get; set; }


        [Required]
        public int GridColumn { get; set; }
    }
}
