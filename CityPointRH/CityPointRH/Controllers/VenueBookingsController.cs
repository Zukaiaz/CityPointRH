using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityPointRH.Data;
using CityPointRH.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CityPointRH.Controllers
{
    public class VenueBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenueBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VenueBookings

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userBookingData = await _context.VenueBookings
                .Include(b => b.Venue)  // Add this line
                .Where(b => b.UserId == userId)
                .ToListAsync();
            return View(userBookingData);
        }

        // GET: VenueBookings/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueBookings = await _context.VenueBookings
                .Include(v => v.Venue)
                .FirstOrDefaultAsync(m => m.VenueBookingsId == id);
            if (venueBookings == null)
            {
                return NotFound();
            }

            return View(venueBookings);
        }

        // GET: VenueBookings/Create

        public IActionResult Create(int venuesId)
        {
            ViewBag.VenuesId = venuesId;
            return View();
        }


        // POST: VenueBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(int VenuesId, DateOnly BookingDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            // Get the current logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                ViewBag.Error = "You must be logged in to make a booking";
                ViewBag.VenuesId = VenuesId;
                return View();
            }

            var venueBookings = new VenueBookings
            {
                VenuesId = VenuesId,
                UserId = userId,
                BookingDate = BookingDate,
                StartTime = StartTime,
                EndTime = EndTime
            };

            try
            {
                _context.Add(venueBookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                ViewBag.Error = $"Error: {error}";
                ViewBag.VenuesId = VenuesId;
                return View();
            }
        }

        // GET: VenueBookings/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueBookings = await _context.VenueBookings.FindAsync(id);
            if (venueBookings == null)
            {
                return NotFound();
            }

            // Change this line - use "VenueName" for display and "VenuesId" for value
            ViewData["VenuesId"] = new SelectList(_context.Venues, "VenuesId", "VenueName", venueBookings.VenuesId);

            return View(venueBookings);
        }

        // POST: VenueBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("VenueBookingsId,VenuesId,UserId,BookingDate,StartTime,EndTime")] VenueBookings venueBookings)
        {
            if (id != venueBookings.VenueBookingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venueBookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueBookingsExists(venueBookings.VenueBookingsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // If we get here, there was an error - reload the dropdown with VenueName
            ViewData["VenuesId"] = new SelectList(_context.Venues, "VenuesId", "VenueName", venueBookings.VenuesId);
            return View(venueBookings);
        }

        // GET: VenueBookings/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueBookings = await _context.VenueBookings
                .Include(v => v.Venue)
                .FirstOrDefaultAsync(m => m.VenueBookingsId == id);
            if (venueBookings == null)
            {
                return NotFound();
            }

            return View(venueBookings);
        }

        // POST: VenueBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venueBookings = await _context.VenueBookings.FindAsync(id);
            if (venueBookings != null)
            {
                _context.VenueBookings.Remove(venueBookings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueBookingsExists(int id)
        {
            return _context.VenueBookings.Any(e => e.VenueBookingsId == id);
        }
    }
}
