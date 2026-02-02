using CityPointRoomHire.Data;

namespace CityPointRoomHire.Models
{
    public class BookingsService
    {
        private readonly ApplicationDbContext _context;
        public BookingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsVenueAvailable(int VenuesId, DateTime StartTime, DateTime EndTime)
        {
            var overlappingBooking = _context.Bookings
                .Where(b => b.VenuesId == VenuesId)
                .Any(b => (StartTime < b.EndTime && EndTime > b.StartTime));

            return !overlappingBooking;
        }
    }
}
