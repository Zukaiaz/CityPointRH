using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CityPointRoomHire.Models
{
    public class Venues
    {
        public int VenuesId { get; set; }
        public string VenueName { get; set; }
        public string VenueDescription { get; set; }
        public string VenueLocation { get; set; }
        public int Capacity { get; set; }
        public float Price { get; set; }
        public ICollection<Bookings>? Bookings { get; set; }
        public ICollection<VenueEquipment>? VenueEquipment { get; set; }
    }
}
