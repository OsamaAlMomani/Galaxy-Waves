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
    public class TeacherProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/TeacherProfiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teacher_Profile.Include(t => t.equipment);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administrator/TeacherProfiles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Teacher_Profile == null)
            {
                return NotFound();
            }

            var teacherProfile = await _context.Teacher_Profile
                .Include(t => t.equipment)
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacherProfile == null)
            {
                return NotFound();
            }

            return View(teacherProfile);
        }

        // GET: Administrator/TeacherProfiles/Create
        public IActionResult Create()
        {
            ViewData["equipmentId"] = new SelectList(_context.equipment, "equipmentId", "Material");
            return View();
        }

        // POST: Administrator/TeacherProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,TeacherFirstName,TeacherLastName,TeacherEmail,TeacherPhone,Img,DOB,Sex,Address,City,Country,JDate,Salary,IBan,DepartmentName,equipmentId")] TeacherProfile teacherProfile)
        {
            if (ModelState.IsValid)
            {
                teacherProfile.TeacherID = Guid.NewGuid();
                _context.Add(teacherProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["equipmentId"] = new SelectList(_context.equipment, "equipmentId", "Material", teacherProfile.equipmentId);
            return View(teacherProfile);
        }

        // GET: Administrator/TeacherProfiles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Teacher_Profile == null)
            {
                return NotFound();
            }

            var teacherProfile = await _context.Teacher_Profile.FindAsync(id);
            if (teacherProfile == null)
            {
                return NotFound();
            }
            ViewData["equipmentId"] = new SelectList(_context.equipment, "equipmentId", "Material", teacherProfile.equipmentId);
            return View(teacherProfile);
        }

        // POST: Administrator/TeacherProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TeacherID,TeacherFirstName,TeacherLastName,TeacherEmail,TeacherPhone,Img,DOB,Sex,Address,City,Country,JDate,Salary,IBan,DepartmentName,equipmentId")] TeacherProfile teacherProfile)
        {
            if (id != teacherProfile.TeacherID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherProfileExists(teacherProfile.TeacherID))
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
            ViewData["equipmentId"] = new SelectList(_context.equipment, "equipmentId", "Material", teacherProfile.equipmentId);
            return View(teacherProfile);
        }

        // GET: Administrator/TeacherProfiles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Teacher_Profile == null)
            {
                return NotFound();
            }

            var teacherProfile = await _context.Teacher_Profile
                .Include(t => t.equipment)
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacherProfile == null)
            {
                return NotFound();
            }

            return View(teacherProfile);
        }

        // POST: Administrator/TeacherProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Teacher_Profile == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Teacher_Profile'  is null.");
            }
            var teacherProfile = await _context.Teacher_Profile.FindAsync(id);
            if (teacherProfile != null)
            {
                _context.Teacher_Profile.Remove(teacherProfile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherProfileExists(Guid id)
        {
          return (_context.Teacher_Profile?.Any(e => e.TeacherID == id)).GetValueOrDefault();
        }
    }
}
