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

        public Venues? Venue { get; set; }
        public ICollection<EquipmentBookings>? EquipmentBookings { get; set; }
    }
}
