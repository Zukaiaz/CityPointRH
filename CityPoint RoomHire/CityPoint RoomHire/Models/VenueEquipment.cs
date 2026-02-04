namespace CityPoint_RoomHire.Models
{
    public class VenueEquipment
    {
        public int VenueEquipmentId { get; set; }
        public int VenueId { get; set; }
        public int EquipmentHireId { get; set; }
        public bool IsIncluded { get; set; } // true = included, false = available as add-on

        public Venues Venue { get; set; }
        public EquipmentHire Equipment { get; set; }
    }
}
