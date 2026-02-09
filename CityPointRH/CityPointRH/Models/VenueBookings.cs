using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityPointRH.Models
{
    public class VenueBookings
    {
        public int VenueBookingsId { get; set; }
        public int VenuesId { get; set; }
        public string UserId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Status { get; set; } = "Pending"; // Add this line

        // Navigation properties
        public Venues Venue { get; set; }
        public IdentityUser User { get; set; }

        public ICollection<EquipmentBookings>? EquipmentBookings { get; set; }

        // Computed property for duration
        [NotMapped]
        public double DurationHours
        {
            get
            {
                var duration = EndTime - StartTime;
                return duration.TotalHours;
            }
        }
    }
}
