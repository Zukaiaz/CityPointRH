namespace CityPoint_RoomHire.Models
{
    public class VenueBookings
    {
        public int VenueBookingsId { get; set; }
        public int VenuesId { get; set; }
        public string UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan  TimeSpanBook { get; set; }

        public Venues?Venue { get; set; }
        public ICollection<EquipmentBookings>? EquipmentBookings { get; set; }
    }
}
