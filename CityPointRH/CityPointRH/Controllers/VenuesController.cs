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

namespace CityPointRH.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Venues
        [Authorize]
        // GET: Venues
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues.ToListAsync();
            return View(venues);
        }

        [Authorize]
        // GET: Venues/Filter
        public async Task<IActionResult> Filter(int? minCapacity, decimal? minPrice, decimal? maxPrice)
        {
            var venues = _context.Venues.AsQueryable();

            if (minCapacity.HasValue)
            {
                venues = venues.Where(v => v.Capacity >= minCapacity.Value);
            }

            if (minPrice.HasValue)
            {
                venues = venues.Where(v => v.PricePerHour >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                venues = venues.Where(v => v.PricePerHour <= maxPrice.Value);
            }

            return View("Index", await venues.ToListAsync());
        }


        // GET: Venues/Details/5
        [Authorize]
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Create(
            [Bind("Name,Address,Description,Capacity,PricePerHour")] Venues venues)
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
        [Authorize]
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
        [Authorize(Roles = "Admin,Staff")]
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
        [Authorize]
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
        [Authorize(Roles = "Admin,Staff")]
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
