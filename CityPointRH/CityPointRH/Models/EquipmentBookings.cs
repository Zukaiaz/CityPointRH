namespace CityPointRH.Models
{
    public class EquipmentBookings
    {
        public int EquipmentBookingsId { get; set; }
        public int VenueBookingsId { get; set; }
        public int EquipmentHireId { get; set; }
        public int Quantity { get; set; } // How many of this item?
        public VenueBookings VenueBooking { get; set; }
        public EquipmentHire Equipment { get; set; }
    }
}
