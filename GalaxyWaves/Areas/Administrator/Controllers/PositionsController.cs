using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galaxy.Core.Models.C_Level.Departments;
using GalaxyWaves.Data;

namespace GalaxyWaves.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PositionsController : Controller
    {
        private readonly AppDbContext _context;

        public PositionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/Positions
        public async Task<IActionResult> Index()
        {
              return _context.Position != null ? 
                          View(await _context.Position.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Position'  is null.");
        }

        // GET: Administrator/Positions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Position == null)
            {
                return NotFound();
            }

            var position = await _context.Position
                .FirstOrDefaultAsync(m => m.positionId == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Administrator/Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("positionId,positionName")] Position position)
        {
            if (ModelState.IsValid)
            {
                position.positionId = Guid.NewGuid();
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Administrator/Positions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Position == null)
            {
                return NotFound();
            }

            var position = await _context.Position.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Administrator/Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("positionId,positionName")] Position position)
        {
            if (id != position.positionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.positionId))
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
            return View(position);
        }

        // GET: Administrator/Positions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Position == null)
            {
                return NotFound();
            }

            var position = await _context.Position
                .FirstOrDefaultAsync(m => m.positionId == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Administrator/Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Position == null)
            {
                return Problem("Entity set 'AppDbContext.Position'  is null.");
            }
            var position = await _context.Position.FindAsync(id);
            if (position != null)
            {
                _context.Position.Remove(position);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(Guid id)
        {
          return (_context.Position?.Any(e => e.positionId == id)).GetValueOrDefault();
        }
    }
}
