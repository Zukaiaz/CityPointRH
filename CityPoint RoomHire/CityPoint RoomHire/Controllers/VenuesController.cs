using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityPoint_RoomHire.Data;
using CityPoint_RoomHire.Models;

namespace CityPoint_RoomHire.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venues.ToListAsync());
        }

        public async Task<IActionResult> FilterByCapacity(int capacity)
        {
            var filteredVenues = await _context.Venues
                .Where(v => v.Capacity >= capacity)
                .ToListAsync();
            return View("Index", filteredVenues);
        }


        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venues = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenuesId == id);
            if (venues == null)
            {
                return NotFound();
            }

            return View(venues);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenuesId,VenueName,VenueDescription,Capacity,PricePerHour")] Venues venues)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venues);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venues);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venues = await _context.Venues.FindAsync(id);
            if (venues == null)
            {
                return NotFound();
            }
            return View(venues);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenuesId,VenueName,VenueDescription,Capacity,PricePerHour")] Venues venues)
        {
            if (id != venues.VenuesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenuesExists(venues.VenuesId))
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
            return View(venues);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venues = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenuesId == id);
            if (venues == null)
            {
                return NotFound();
            }

            return View(venues);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venues = await _context.Venues.FindAsync(id);
            if (venues != null)
            {
                _context.Venues.Remove(venues);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenuesExists(int id)
        {
            return _context.Venues.Any(e => e.VenuesId == id);
        }
    }
}
