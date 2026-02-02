using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityPointRoomHire.Data;
using CityPointRoomHire.Models;
using Microsoft.AspNetCore.Authorization;


namespace CityPointRoomHire.Models
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bookings.Include(b => b.Venues);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Venues)
                .FirstOrDefaultAsync(m => m.BookingsId == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["VenuesId"] = new SelectList(_context.Venues, "VenuesId", "VenuesId");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingsId,FirstName,Surname,Birthday,StartTime,EndTime,VenuesId")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                if (!IsVenueAvailable(bookings.VenuesId,bookings.StartTime,bookings.EndTime))
                {
                    ModelState.AddModelError(
                        "",
                        "This venue is already booked for the selected time."
                    );
                }

                if (bookings.EndTime <= bookings.StartTime)
                {
                    ModelState.AddModelError(
                        "",
                        "End time must be after start time."
                    );
                }

                if (ModelState.IsValid)
                {
                    _context.Add(bookings);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            ViewData["VenuesId"] = new SelectList(_context.Venues, "VenuesId", "VenuesId", bookings.VenuesId);
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["VenuesId"] = new SelectList(_context.Venues, "VenuesId", "VenuesId", bookings.VenuesId);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingsId,FirstName,Surname,Birthday,StartTime,EndTime,VenuesId")] Bookings bookings)
        {
            if (id != bookings.BookingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.BookingsId))
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
            ViewData["VenuesId"] = new SelectList(_context.Venues, "VenuesId", "VenuesId", bookings.VenuesId);
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Venues)
                .FirstOrDefaultAsync(m => m.BookingsId == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingsId == id);
        }
        public bool IsVenueAvailable(int venueId, DateTime startTime, DateTime endTime)
        {
            return !_context.Bookings.Any(b =>
                b.VenuesId == venueId &&
                startTime < b.EndTime &&
                endTime > b.StartTime
            );
        }

    }
}
