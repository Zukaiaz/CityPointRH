using CityPointRoomHire.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CityPointRoomHire.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Venues> Venues { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<VenueEquipment> VenueEquipments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite key for junction table
            modelBuilder.Entity<VenueEquipment>()
                .HasKey(ve => new { ve.VenueId, ve.EquipmentId });

            // Configure relationships
            modelBuilder.Entity<VenueEquipment>()
                .HasOne(ve => ve.Venue)
                .WithMany(v => v.VenueEquipment)
                .HasForeignKey(ve => ve.VenueId);

            modelBuilder.Entity<VenueEquipment>()
                .HasOne(ve => ve.Equipment)
                .WithMany(e => e.VenueEquipment)
                .HasForeignKey(ve => ve.EquipmentId);
        }
    }
}
