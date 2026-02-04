namespace CityPoint_RoomHire.Models
{
    public class Venues
    {
        public int VenuesId { get; set; }
        public string VenueName { get; set; }
        public string VenueDescription { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }

        public ICollection<VenueBookings>? VenueBookings { get; set; }
        public ICollection<VenueEquipment>? VenueEquipment { get; set; }
    }
}