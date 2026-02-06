using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CityPointRH.Models;

namespace CityPointRH.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CityPointRH.Models.EquipmentBookings> EquipmentBookings { get; set; } = default!;
        public DbSet<CityPointRH.Models.EquipmentHire> EquipmentHire { get; set; } = default!;
        public DbSet<CityPointRH.Models.VenueBookings> VenueBookings { get; set; } = default!;
        public DbSet<CityPointRH.Models.VenueEquipment> VenueEquipment { get; set; } = default!;
        public DbSet<CityPointRH.Models.Venues> Venues { get; set; } = default!;
    }
}
