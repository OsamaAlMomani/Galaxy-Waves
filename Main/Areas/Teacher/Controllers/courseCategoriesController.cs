using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Main.Areas.Teacher.Models.Courses;
using Main.DataAccess;

namespace Main.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class courseCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public courseCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Teacher/courseCategories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.courseCategories.Include(c => c.core);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Teacher/courseCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.courseCategories == null)
            {
                return NotFound();
            }

            var courseCategory = await _context.courseCategories
                .Include(c => c.core)
                .FirstOrDefaultAsync(m => m.courseCategory_Id == id);
            if (courseCategory == null)
            {
                return NotFound();
            }

            return View(courseCategory);
        }

        // GET: Teacher/courseCategories/Create
        public IActionResult Create()
        {
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName");
            return View();
        }

        // POST: Teacher/courseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("courseCategory_Id,courseCategory_Name,courseCategory_Description,courseCategory_Img,courseCategory_Price,CoreId")] courseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                courseCategory.courseCategory_Id = Guid.NewGuid();
                _context.Add(courseCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName", courseCategory.CoreId);
            return View(courseCategory);
        }

        // GET: Teacher/courseCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.courseCategories == null)
            {
                return NotFound();
            }

            var courseCategory = await _context.courseCategories.FindAsync(id);
            if (courseCategory == null)
            {
                return NotFound();
            }
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName", courseCategory.CoreId);
            return View(courseCategory);
        }

        // POST: Teacher/courseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("courseCategory_Id,courseCategory_Name,courseCategory_Description,courseCategory_Img,courseCategory_Price,CoreId")] courseCategory courseCategory)
        {
            if (id != courseCategory.courseCategory_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!courseCategoryExists(courseCategory.courseCategory_Id))
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
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName", courseCategory.CoreId);
            return View(courseCategory);
        }

        // GET: Teacher/courseCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.courseCategories == null)
            {
                return NotFound();
            }

            var courseCategory = await _context.courseCategories
                .Include(c => c.core)
                .FirstOrDefaultAsync(m => m.courseCategory_Id == id);
            if (courseCategory == null)
            {
                return NotFound();
            }

            return View(courseCategory);
        }

        // POST: Teacher/courseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.courseCategories == null)
            {
                return Problem("Entity set 'AppDbContext.courseCategories'  is null.");
            }
            var courseCategory = await _context.courseCategories.FindAsync(id);
            if (courseCategory != null)
            {
                _context.courseCategories.Remove(courseCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool courseCategoryExists(Guid id)
        {
          return (_context.courseCategories?.Any(e => e.courseCategory_Id == id)).GetValueOrDefault();
        }
    }
}
