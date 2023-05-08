using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Main.Areas.Departments.Models;
using Main.DataAccess;

namespace Main.Areas.Departments.Controllers
{
    [Area("Departments")]
    public class SpecializationsController : Controller
    {
        private readonly AppDbContext _context;

        public SpecializationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Departments/Specializations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.specialization.Include(s => s.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Departments/Specializations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.specialization == null)
            {
                return NotFound();
            }

            var specialization = await _context.specialization
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SpecId == id);
            if (specialization == null)
            {
                return NotFound();
            }

            return View(specialization);
        }

        // GET: Departments/Specializations/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "categoryName");
            return View();
        }

        // POST: Departments/Specializations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecId,SpecName,CategoryId")] Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "categoryName", specialization.CategoryId);
            return View(specialization);
        }

        // GET: Departments/Specializations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.specialization == null)
            {
                return NotFound();
            }

            var specialization = await _context.specialization.FindAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "categoryName", specialization.CategoryId);
            return View(specialization);
        }

        // POST: Departments/Specializations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecId,SpecName,CategoryId")] Specialization specialization)
        {
            if (id != specialization.SpecId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecializationExists(specialization.SpecId))
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
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "categoryName", specialization.CategoryId);
            return View(specialization);
        }

        // GET: Departments/Specializations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.specialization == null)
            {
                return NotFound();
            }

            var specialization = await _context.specialization
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SpecId == id);
            if (specialization == null)
            {
                return NotFound();
            }

            return View(specialization);
        }

        // POST: Departments/Specializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.specialization == null)
            {
                return Problem("Entity set 'AppDbContext.specialization'  is null.");
            }
            var specialization = await _context.specialization.FindAsync(id);
            if (specialization != null)
            {
                _context.specialization.Remove(specialization);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecializationExists(int id)
        {
          return (_context.specialization?.Any(e => e.SpecId == id)).GetValueOrDefault();
        }
    }
}
