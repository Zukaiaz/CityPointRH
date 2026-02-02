using CityPointRoomHire.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CityPointRoomHire.Data
{
    public class SeedData
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Manager", "User" };
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
            if(!await userManager.IsInRoleAsync(adminUser,"Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
        public static async Task SeedVenuesAsync(ApplicationDbContext context)
        {
            if (!await context.Venues.AnyAsync())
            {
                var venues = new List<Venues>
                {
                    new Venues
                    {
                        VenueName = "The Brewery",
                        VenueLocation = "London",
                        VenueDescription = "Historic venue in central London",
                        Capacity = 1000,
                        Price = 5000f
                    },
                    new Venues
                    {
                        VenueName = "Skyline Lounge",
                        VenueLocation = "London",
                        VenueDescription = "Intimate lounge with city views",
                        Capacity = 60,
                        Price = 800f
                    },
                    new Venues
                    {
                        VenueName = "Business Design Centre",
                        VenueLocation = "Islington, London",
                        VenueDescription = "A versatile conference and exhibition venue in the heart of London",
                        Capacity = 600,
                        Price = 3000f,
                    },
                    new Venues
                    {
                        VenueName = "Manchester Central Convention Complex",
                        VenueLocation = "Manchester",
                        VenueDescription = "Modern conference centre with large halls and breakout rooms",
                        Capacity = 3000,
                        Price = 8000f,
                    },
                    new Venues
                    {
                        VenueName = "Garden Pavilion",
                        VenueLocation = "Brighton",
                        VenueDescription = "Outdoor pavilion suitable for weddings and private events",
                        Capacity = 100,
                        Price = 1500f,
                    }
                };
                await context.Venues.AddRangeAsync(venues);
                await context.SaveChangesAsync();
            }
            if (!await context.Equipment.AnyAsync())
            {
                var projector = new Equipment { Name = "Projector" };
                var microphone = new Equipment { Name = "Microphone" };
                var chairs = new Equipment { Name = "Chairs" };
                var speakers = new Equipment { Name = "Speakers" };
                var whiteboard = new Equipment { Name = "Whiteboard" };

                await context.Equipment.AddRangeAsync(projector, microphone, chairs, speakers, whiteboard);
                await context.SaveChangesAsync();

                // 3️⃣ Link equipment to venues
                var brewery = await context.Venues.FirstAsync(v => v.VenueName == "The Brewery");
                var skyline = await context.Venues.FirstAsync(v => v.VenueName == "Skyline Lounge");
                var businessDesign = await context.Venues.FirstAsync(v => v.VenueName == "Business Design Centre");
                var manchesterCentral = await context.Venues.FirstAsync(v => v.VenueName == "Manchester Central Convention Complex");
                var gardenPavilion = await context.Venues.FirstAsync(v => v.VenueName == "Garden Pavilion");

                await context.VenueEquipments.AddRangeAsync(
                    // The Brewery
                    new VenueEquipment { Venue = brewery, Equipment = projector, Quantity = 1 },
                    new VenueEquipment { Venue = brewery, Equipment = microphone, Quantity = 2 },
                    new VenueEquipment { Venue = brewery, Equipment = chairs, Quantity = 1000 },
                    new VenueEquipment { Venue = brewery, Equipment = speakers, Quantity = 2 },

                    // Skyline Lounge
                    new VenueEquipment { Venue = skyline, Equipment = chairs, Quantity = 60 },
                    new VenueEquipment { Venue = skyline, Equipment = microphone, Quantity = 1 },

                    // Business Design Centre
                    new VenueEquipment { Venue = businessDesign, Equipment = projector, Quantity = 1 },
                    new VenueEquipment { Venue = businessDesign, Equipment = chairs, Quantity = 600 },
                    new VenueEquipment { Venue = businessDesign, Equipment = whiteboard, Quantity = 2 },

                    // Manchester Central
                    new VenueEquipment { Venue = manchesterCentral, Equipment = projector, Quantity = 3 },
                    new VenueEquipment { Venue = manchesterCentral, Equipment = microphone, Quantity = 4 },
                    new VenueEquipment { Venue = manchesterCentral, Equipment = chairs, Quantity = 3000 },
                    new VenueEquipment { Venue = manchesterCentral, Equipment = speakers, Quantity = 4 },

                    // Garden Pavilion
                    new VenueEquipment { Venue = gardenPavilion, Equipment = chairs, Quantity = 100 },
                    new VenueEquipment { Venue = gardenPavilion, Equipment = microphone, Quantity = 1 },
                    new VenueEquipment { Venue = gardenPavilion, Equipment = speakers, Quantity = 2 }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
