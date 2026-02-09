using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CityPointRH.Data
{
    public class SeedData
    {
        public static async Task Seedvenues(ApplicationDbContext context)
        {
            if (!await context.Venues.AnyAsync())
            {
                var venues = new List<Models.Venues>
                   {
                       new Models.Venues { VenueName = "Conference Room A", VenueDescription = "A spacious conference room with seating for 50 people.", Capacity = 50, PricePerHour = 100.00m, ImagePath="meeting-room.jpg"  },
                       new Models.Venues { VenueName = "Meeting Room B", VenueDescription = "A small meeting room suitable for up to 10 people.", Capacity = 10, PricePerHour = 40.00m, ImagePath = "meeting-roomB.jpg" },
                       new Models.Venues { VenueName = "Auditorium", VenueDescription = "A large auditorium with stage and seating for 200 people.", Capacity = 200, PricePerHour = 300.00m, ImagePath = "auditorium.jpg" },
                       new Models.Venues { VenueName = "Riverside Pavillion", VenueDescription = "An elegant, two-storey, Grade II listed building (originally 1913) overlooking the Thames", Capacity = 350, PricePerHour = 460m, ImagePath = "riverside.jpg" },
                       new Models.Venues { VenueName = "City Point Conference Centre", VenueDescription = "A modern conference centre with multiple rooms and state-of-the-art facilities.", Capacity = 500, PricePerHour = 600.00m, ImagePath = "conference.jpg" },
                       new Models.Venues { VenueName = "Skyline Rooftop", VenueDescription = "A rooftop venue with stunning city views, perfect for events and parties.", Capacity = 150, PricePerHour = 200.00m, ImagePath = "rooftop.jpg" },
                       new Models.Venues { VenueName = "Garden Terrace", VenueDescription = "A beautiful outdoor terrace surrounded by gardens, ideal for weddings and receptions.", Capacity = 100, PricePerHour = 150.00m, ImagePath = "garden.jpg" }, 
                       new Models.Venues { VenueName = "Bethrothed Suite", VenueDescription = "A suite at the highest floor of a sky scraper with a wonderful view of the city", Capacity = 20, PricePerHour = 219.99m, ImagePath = "suite.jpg" },
                       new Models.Venues { VenueName = "The Refinery Room", VenueDescription = "A room that's perfect for recreational activities, wonderful for all ages", Capacity = 35, PricePerHour = 29.99m, ImagePath = "refinery.jpg"}
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

        public static async Task SeedRoles(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Staff", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(adminUser, "Admin@123");
            }
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var staffUser = await userManager.FindByEmailAsync("staff@example.com");
            if (staffUser == null)
            {
                staffUser = new IdentityUser { UserName = "staff@example.com", Email = "staff@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(staffUser, "Staff@123");
            }
            if (!await userManager.IsInRoleAsync(staffUser, "Staff"))
            {
                await userManager.AddToRoleAsync(staffUser, "Staff");
            }

            var normalUser = await userManager.FindByEmailAsync("user@example.com");
            if (normalUser == null)
            {
                normalUser = new IdentityUser { UserName = "user@example.com", Email = "user@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(normalUser, "User@123");
            }
            if (!await userManager.IsInRoleAsync(normalUser, "User"))
            {
                await userManager.AddToRoleAsync(normalUser, "User");
            }

        }
    }
}