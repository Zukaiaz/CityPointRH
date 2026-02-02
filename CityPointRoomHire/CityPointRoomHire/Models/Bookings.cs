using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityPointRoomHire.Models
{
    public class Bookings
    {
        public int BookingsId { get; set; } //PK
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Range(typeof(DateTime),"01/01/1900","01/01/2100",ErrorMessage = "invalid")]
        public DateOnly Birthday { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [ForeignKey("VenuesId")]
        public int VenuesId { get; set; }
        public Venues Venues { get; set; } // Singular reference - each booking linked to one Venue

        public bool IsValidBooking()
        {
            return EndTime > StartTime;
        }

    }
}
