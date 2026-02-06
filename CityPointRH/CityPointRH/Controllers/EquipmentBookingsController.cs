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
    public class EquipmentBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipmentBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EquipmentBookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EquipmentBookings.Include(e => e.Equipment).Include(e => e.VenueBooking);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EquipmentBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentBookings = await _context.EquipmentBookings
                .Include(e => e.Equipment)
                .Include(e => e.VenueBooking)
                .FirstOrDefaultAsync(m => m.EquipmentBookingsId == id);
            if (equipmentBookings == null)
            {
                return NotFound();
            }

            return View(equipmentBookings);
        }

        // GET: EquipmentBookings/Create
        public IActionResult Create()
        {
            ViewData["EquipmentHireId"] = new SelectList(_context.Set<EquipmentHire>(), "EquipmentHireId", "EquipmentHireId");
            ViewData["VenueBookingsId"] = new SelectList(_context.Set<VenueBookings>(), "VenueBookingsId", "VenueBookingsId");
            return View();
        }

        // POST: EquipmentBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentBookingsId,VenueBookingsId,EquipmentHireId,Quantity")] EquipmentBookings equipmentBookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentBookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentHireId"] = new SelectList(_context.Set<EquipmentHire>(), "EquipmentHireId", "EquipmentHireId", equipmentBookings.EquipmentHireId);
            ViewData["VenueBookingsId"] = new SelectList(_context.Set<VenueBookings>(), "VenueBookingsId", "VenueBookingsId", equipmentBookings.VenueBookingsId);
            return View(equipmentBookings);
        }

        // GET: EquipmentBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentBookings = await _context.EquipmentBookings.FindAsync(id);
            if (equipmentBookings == null)
            {
                return NotFound();
            }
            ViewData["EquipmentHireId"] = new SelectList(_context.Set<EquipmentHire>(), "EquipmentHireId", "EquipmentHireId", equipmentBookings.EquipmentHireId);
            ViewData["VenueBookingsId"] = new SelectList(_context.Set<VenueBookings>(), "VenueBookingsId", "VenueBookingsId", equipmentBookings.VenueBookingsId);
            return View(equipmentBookings);
        }

        // POST: EquipmentBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipmentBookingsId,VenueBookingsId,EquipmentHireId,Quantity")] EquipmentBookings equipmentBookings)
        {
            if (id != equipmentBookings.EquipmentBookingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentBookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentBookingsExists(equipmentBookings.EquipmentBookingsId))
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
            ViewData["EquipmentHireId"] = new SelectList(_context.Set<EquipmentHire>(), "EquipmentHireId", "EquipmentHireId", equipmentBookings.EquipmentHireId);
            ViewData["VenueBookingsId"] = new SelectList(_context.Set<VenueBookings>(), "VenueBookingsId", "VenueBookingsId", equipmentBookings.VenueBookingsId);
            return View(equipmentBookings);
        }

        // GET: EquipmentBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentBookings = await _context.EquipmentBookings
                .Include(e => e.Equipment)
                .Include(e => e.VenueBooking)
                .FirstOrDefaultAsync(m => m.EquipmentBookingsId == id);
            if (equipmentBookings == null)
            {
                return NotFound();
            }

            return View(equipmentBookings);
        }

        // POST: EquipmentBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipmentBookings = await _context.EquipmentBookings.FindAsync(id);
            if (equipmentBookings != null)
            {
                _context.EquipmentBookings.Remove(equipmentBookings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentBookingsExists(int id)
        {
            return _context.EquipmentBookings.Any(e => e.EquipmentBookingsId == id);
        }
    }
}
