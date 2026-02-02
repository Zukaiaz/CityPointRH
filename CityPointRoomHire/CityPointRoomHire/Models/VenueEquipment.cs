namespace CityPointRoomHire.Models
{
    public class VenueEquipment
    {
        public int VenueId { get; set; }
        public Venues Venue { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public int Quantity { get; set; } = 1; // optional
    }
}
