using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CityPoint_RoomHire.Models;

namespace CityPoint_RoomHire.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CityPoint_RoomHire.Models.Venues> Venues { get; set; } = default!;
        public DbSet<CityPoint_RoomHire.Models.VenueEquipment> VenueEquipment { get; set; } = default!;
        public DbSet<CityPoint_RoomHire.Models.VenueBookings> VenueBookings { get; set; } = default!;
        public DbSet<CityPoint_RoomHire.Models.EquipmentHire> EquipmentHire { get; set; } = default!;
        public DbSet<CityPoint_RoomHire.Models.EquipmentBookings> EquipmentBookings { get; set; } = default!;
    }
}
