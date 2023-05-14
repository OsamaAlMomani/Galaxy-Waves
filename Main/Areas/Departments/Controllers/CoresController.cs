using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Main.DataAccess;
using Main.Models.DepartmentModels;

namespace Main.Areas.Departments.Controllers
{
    [Area("Departments")]
    public class CoresController : Controller
    {
        private readonly AppDbContext _context;

        public CoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Departments/Cores
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.cores.Include(c => c.spec);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Departments/Cores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.cores == null)
            {
                return NotFound();
            }

            var core = await _context.cores
                .Include(c => c.spec)
                .FirstOrDefaultAsync(m => m.CoreId == id);
            if (core == null)
            {
                return NotFound();
            }

            return View(core);
        }

        // GET: Departments/Cores/Create
        public IActionResult Create()
        {
            ViewData["SpecId"] = new SelectList(_context.specialization, "SpecId", "SpecName");
            return View();
        }

        // POST: Departments/Cores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoreId,CoreName,SpecId")] Core core)
        {
            if (ModelState.IsValid)
            {
                core.CoreId = Guid.NewGuid();
                _context.Add(core);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecId"] = new SelectList(_context.specialization, "SpecId", "SpecName", core.SpecId);
            return View(core);
        }

        // GET: Departments/Cores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.cores == null)
            {
                return NotFound();
            }

            var core = await _context.cores.FindAsync(id);
            if (core == null)
            {
                return NotFound();
            }
            ViewData["SpecId"] = new SelectList(_context.specialization, "SpecId", "SpecName", core.SpecId);
            return View(core);
        }

        // POST: Departments/Cores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CoreId,CoreName,SpecId")] Core core)
        {
            if (id != core.CoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(core);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoreExists(core.CoreId))
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
            ViewData["SpecId"] = new SelectList(_context.specialization, "SpecId", "SpecName", core.SpecId);
            return View(core);
        }

        // GET: Departments/Cores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.cores == null)
            {
                return NotFound();
            }

            var core = await _context.cores
                .Include(c => c.spec)
                .FirstOrDefaultAsync(m => m.CoreId == id);
            if (core == null)
            {
                return NotFound();
            }

            return View(core);
        }

        // POST: Departments/Cores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.cores == null)
            {
                return Problem("Entity set 'AppDbContext.cores'  is null.");
            }
            var core = await _context.cores.FindAsync(id);
            if (core != null)
            {
                _context.cores.Remove(core);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoreExists(Guid id)
        {
          return (_context.cores?.Any(e => e.CoreId == id)).GetValueOrDefault();
        }
    }
}
