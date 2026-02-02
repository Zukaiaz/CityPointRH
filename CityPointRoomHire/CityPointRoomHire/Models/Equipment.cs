using System.ComponentModel.DataAnnotations;

namespace CityPointRoomHire.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        [Required]
        public string Name { get; set; }   
        public ICollection<VenueEquipment>? VenueEquipment { get; set; }
    }
}
