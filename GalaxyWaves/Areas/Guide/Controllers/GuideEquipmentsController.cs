using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galaxy.Core.Models.Guide;
using GalaxyWaves.Data;

namespace GalaxyWaves.Areas.Guide.Controllers
{
    [Area("Guide")]
    public class GuideEquipmentsController : Controller
    {
        private readonly AppDbContext _context;

        public GuideEquipmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Guide/GuideEquipments
        public async Task<IActionResult> Index()
        {
              return _context.guideEquipment != null ? 
                          View(await _context.guideEquipment.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.guideEquipment'  is null.");
        }

        // GET: Guide/GuideEquipments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.guideEquipment == null)
            {
                return NotFound();
            }

            var guideEquipment = await _context.guideEquipment
                .FirstOrDefaultAsync(m => m.equipmentId == id);
            if (guideEquipment == null)
            {
                return NotFound();
            }

            return View(guideEquipment);
        }

        // GET: Guide/GuideEquipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guide/GuideEquipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("equipmentId,equipmentName,equipmentImg,equipmentColor,equipmentType,equipmentBrand,Material,Price,Hidden")] GuideEquipment guideEquipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guideEquipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guideEquipment);
        }

        // GET: Guide/GuideEquipments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.guideEquipment == null)
            {
                return NotFound();
            }

            var guideEquipment = await _context.guideEquipment.FindAsync(id);
            if (guideEquipment == null)
            {
                return NotFound();
            }
            return View(guideEquipment);
        }

        // POST: Guide/GuideEquipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("equipmentId,equipmentName,equipmentImg,equipmentColor,equipmentType,equipmentBrand,Material,Price,Hidden")] GuideEquipment guideEquipment)
        {
            if (id != guideEquipment.equipmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guideEquipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuideEquipmentExists(guideEquipment.equipmentId))
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
            return View(guideEquipment);
        }

        // GET: Guide/GuideEquipments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.guideEquipment == null)
            {
                return NotFound();
            }

            var guideEquipment = await _context.guideEquipment
                .FirstOrDefaultAsync(m => m.equipmentId == id);
            if (guideEquipment == null)
            {
                return NotFound();
            }

            return View(guideEquipment);
        }

        // POST: Guide/GuideEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (_context.guideEquipment == null)
            {
                return Problem("Entity set 'AppDbContext.guideEquipment'  is null.");
            }
            var guideEquipment = await _context.guideEquipment.FindAsync(id);
            if (guideEquipment != null)
            {
                _context.guideEquipment.Remove(guideEquipment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuideEquipmentExists(Guid? id)
        {
          return (_context.guideEquipment?.Any(e => e.equipmentId == id)).GetValueOrDefault();
        }
    }
}
