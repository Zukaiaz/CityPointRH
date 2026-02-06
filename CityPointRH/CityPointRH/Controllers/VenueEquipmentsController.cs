using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityPointRH.Data;
using CityPointRH.Models;

namespace CityPointRH.Controllers
{
    public class VenueEquipmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenueEquipmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VenueEquipments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VenueEquipment.Include(v => v.Equipment).Include(v => v.Venue);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VenueEquipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueEquipment = await _context.VenueEquipment
                .Include(v => v.Equipment)
                .Include(v => v.Venue)
                .FirstOrDefaultAsync(m => m.VenueEquipmentId == id);
            if (venueEquipment == null)
            {
                return NotFound();
            }

            return View(venueEquipment);
        }

        // GET: VenueEquipments/Create
        public IActionResult Create()
        {
            ViewData["EquipmentHireId"] = new SelectList(_context.EquipmentHire, "EquipmentHireId", "EquipmentHireId");
            ViewData["VenueId"] = new SelectList(_context.Set<Venues>(), "VenuesId", "VenuesId");
            return View();
        }

        // POST: VenueEquipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueEquipmentId,VenueId,EquipmentHireId,IsIncluded")] VenueEquipment venueEquipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venueEquipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentHireId"] = new SelectList(_context.EquipmentHire, "EquipmentHireId", "EquipmentHireId", venueEquipment.EquipmentHireId);
            ViewData["VenueId"] = new SelectList(_context.Set<Venues>(), "VenuesId", "VenuesId", venueEquipment.VenueId);
            return View(venueEquipment);
        }

        // GET: VenueEquipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueEquipment = await _context.VenueEquipment.FindAsync(id);
            if (venueEquipment == null)
            {
                return NotFound();
            }
            ViewData["EquipmentHireId"] = new SelectList(_context.EquipmentHire, "EquipmentHireId", "EquipmentHireId", venueEquipment.EquipmentHireId);
            ViewData["VenueId"] = new SelectList(_context.Set<Venues>(), "VenuesId", "VenuesId", venueEquipment.VenueId);
            return View(venueEquipment);
        }

        // POST: VenueEquipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueEquipmentId,VenueId,EquipmentHireId,IsIncluded")] VenueEquipment venueEquipment)
        {
            if (id != venueEquipment.VenueEquipmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venueEquipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueEquipmentExists(venueEquipment.VenueEquipmentId))
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
            ViewData["EquipmentHireId"] = new SelectList(_context.EquipmentHire, "EquipmentHireId", "EquipmentHireId", venueEquipment.EquipmentHireId);
            ViewData["VenueId"] = new SelectList(_context.Set<Venues>(), "VenuesId", "VenuesId", venueEquipment.VenueId);
            return View(venueEquipment);
        }

        // GET: VenueEquipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueEquipment = await _context.VenueEquipment
                .Include(v => v.Equipment)
                .Include(v => v.Venue)
                .FirstOrDefaultAsync(m => m.VenueEquipmentId == id);
            if (venueEquipment == null)
            {
                return NotFound();
            }

            return View(venueEquipment);
        }

        // POST: VenueEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venueEquipment = await _context.VenueEquipment.FindAsync(id);
            if (venueEquipment != null)
            {
                _context.VenueEquipment.Remove(venueEquipment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueEquipmentExists(int id)
        {
            return _context.VenueEquipment.Any(e => e.VenueEquipmentId == id);
        }
    }
}
