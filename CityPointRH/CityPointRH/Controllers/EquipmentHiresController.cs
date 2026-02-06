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
    public class EquipmentHiresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipmentHiresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EquipmentHires
        public async Task<IActionResult> Index()
        {
            return View(await _context.EquipmentHire.ToListAsync());
        }

        // GET: EquipmentHires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentHire = await _context.EquipmentHire
                .FirstOrDefaultAsync(m => m.EquipmentHireId == id);
            if (equipmentHire == null)
            {
                return NotFound();
            }

            return View(equipmentHire);
        }

        // GET: EquipmentHires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EquipmentHires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentHireId,EquipmentName,EquipmentDescription,PricePerHour")] EquipmentHire equipmentHire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentHire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipmentHire);
        }

        // GET: EquipmentHires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentHire = await _context.EquipmentHire.FindAsync(id);
            if (equipmentHire == null)
            {
                return NotFound();
            }
            return View(equipmentHire);
        }

        // POST: EquipmentHires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipmentHireId,EquipmentName,EquipmentDescription,PricePerHour")] EquipmentHire equipmentHire)
        {
            if (id != equipmentHire.EquipmentHireId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentHire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentHireExists(equipmentHire.EquipmentHireId))
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
            return View(equipmentHire);
        }

        // GET: EquipmentHires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentHire = await _context.EquipmentHire
                .FirstOrDefaultAsync(m => m.EquipmentHireId == id);
            if (equipmentHire == null)
            {
                return NotFound();
            }

            return View(equipmentHire);
        }

        // POST: EquipmentHires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipmentHire = await _context.EquipmentHire.FindAsync(id);
            if (equipmentHire != null)
            {
                _context.EquipmentHire.Remove(equipmentHire);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentHireExists(int id)
        {
            return _context.EquipmentHire.Any(e => e.EquipmentHireId == id);
        }
    }
}
