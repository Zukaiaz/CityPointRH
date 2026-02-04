using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CityPoint_RoomHire.Data
{
    public static class SeedData
    {
        public static async Task Seedvenues(ApplicationDbContext context)
        {
            if (!await context.Venues.AnyAsync())
            {
                var venues = new List<Models.Venues>
                   {
                       new Models.Venues { VenueName = "Conference Room A", VenueDescription = "A spacious conference room with seating for 50 people.", Capacity = 50, PricePerHour = 100.00m },
                       new Models.Venues { VenueName = "Meeting Room B", VenueDescription = "A small meeting room suitable for up to 10 people.", Capacity = 10, PricePerHour = 40.00m },
                       new Models.Venues { VenueName = "Auditorium", VenueDescription = "A large auditorium with stage and seating for 200 people.", Capacity = 200, PricePerHour = 300.00m }
                   };
                await context.Venues.AddRangeAsync(venues);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedEquipmentHire(ApplicationDbContext context)
        {
            if (!await context.EquipmentHire.AnyAsync())
            {
                var equipmentHires = new List<Models.EquipmentHire>
                   {
                       new Models.EquipmentHire { EquipmentName = "Projector", EquipmentDescription = "High-resolution projector suitable for presentations.", PricePerHour = 25.00m },
                       new Models.EquipmentHire { EquipmentName = "Sound System", EquipmentDescription = "Complete sound system with microphones and speakers.", PricePerHour = 50.00m },
                       new Models.EquipmentHire { EquipmentName = "Whiteboard", EquipmentDescription = "Large whiteboard with markers and erasers.", PricePerHour = 15.00m }
                   };
                await context.EquipmentHire.AddRangeAsync(equipmentHires);
                await context.SaveChangesAsync();
            }
        }
    }
}
