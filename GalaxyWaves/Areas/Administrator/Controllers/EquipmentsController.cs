using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galaxy.Core.Models.AdminSite;
using GalaxyWaves.Data;

namespace GalaxyWaves.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class EquipmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/Equipments
        public async Task<IActionResult> Index()
        {
              return _context.equipment != null ? 
                          View(await _context.equipment.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.equipment'  is null.");
        }

        // GET: Administrator/Equipments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.equipment == null)
            {
                return NotFound();
            }

            var equipment = await _context.equipment
                .FirstOrDefaultAsync(m => m.equipmentId == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Administrator/Equipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Equipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("equipmentId,equipmentName,equipmentImg,equipmentColor,equipmentType,equipmentBrand,Material,Price,Hidden")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Administrator/Equipments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.equipment == null)
            {
                return NotFound();
            }

            var equipment = await _context.equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            return View(equipment);
        }

        // POST: Administrator/Equipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("equipmentId,equipmentName,equipmentImg,equipmentColor,equipmentType,equipmentBrand,Material,Price,Hidden")] Equipment equipment)
        {
            if (id != equipment.equipmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.equipmentId))
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
            return View(equipment);
        }

        // GET: Administrator/Equipments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.equipment == null)
            {
                return NotFound();
            }

            var equipment = await _context.equipment
                .FirstOrDefaultAsync(m => m.equipmentId == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Administrator/Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (_context.equipment == null)
            {
                return Problem("Entity set 'ApplicationDbContext.equipment'  is null.");
            }
            var equipment = await _context.equipment.FindAsync(id);
            if (equipment != null)
            {
                _context.equipment.Remove(equipment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(Guid? id)
        {
          return (_context.equipment?.Any(e => e.equipmentId == id)).GetValueOrDefault();
        }
    }
}
