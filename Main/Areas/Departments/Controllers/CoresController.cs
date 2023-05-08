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
              return _context.cores != null ? 
                          View(await _context.cores.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.cores'  is null.");
        }

        // GET: Departments/Cores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.cores == null)
            {
                return NotFound();
            }

            var core = await _context.cores
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
                _context.Add(core);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(core);
        }

        // GET: Departments/Cores/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(core);
        }

        // POST: Departments/Cores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoreId,CoreName,SpecId")] Core core)
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
            return View(core);
        }

        // GET: Departments/Cores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.cores == null)
            {
                return NotFound();
            }

            var core = await _context.cores
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        private bool CoreExists(int id)
        {
          return (_context.cores?.Any(e => e.CoreId == id)).GetValueOrDefault();
        }
    }
}
