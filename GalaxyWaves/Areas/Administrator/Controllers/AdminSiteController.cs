using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galaxy.Core.Models.C_Level.Profile;
using GalaxyWaves.Data;
using Microsoft.AspNetCore.Authorization;

namespace GalaxyWaves.Areas.Administrator.Controllers
{
    //[Authorize("Administrator")]
    [Area("Administrator")]
    public class AdminSiteController : Controller
    {
        private readonly AppDbContext _context;

        public AdminSiteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/AdminSite
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.C_Level.Include(c => c.equipment);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Administrator/AdminSite/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.C_Level == null)
            {
                return NotFound();
            }

            var cLVLProfile = await _context.C_Level
                .Include(c => c.equipment)
                .FirstOrDefaultAsync(m => m.cID == id);
            if (cLVLProfile == null)
            {
                return NotFound();
            }

            return View(cLVLProfile);
        }

        // GET: Administrator/AdminSite/Create
        public IActionResult Create()
        {
            ViewData["equipmentId"] = new SelectList(_context.equipment, "equipmentId", "Material");
            return View();
        }

        // POST: Administrator/AdminSite/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cID,cFirstName,cLastName,cEmail,cPhone,Img,DOB,Sex,Address,City,Country,JDate,Salary,Password,IBan,DepartmentName,positionName,equipmentId")] CLVLProfile cLVLProfile)
        {
            if (ModelState.IsValid)
            {
                cLVLProfile.cID = Guid.NewGuid();
                _context.Add(cLVLProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["equipmentId"] = new SelectList(_context.equipment, "equipmentId", "Material", cLVLProfile.equipmentId);
            return View(cLVLProfile);
        }

        // GET: Administrator/AdminSite/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.C_Level == null)
            {
                return NotFound();
            }

            var cLVLProfile = await _context.C_Level.FindAsync(id);
            if (cLVLProfile == null)
            {
                return NotFound();
            }
            ViewData["equipmentId"] = new SelectList(_context.equipment, "equipmentId", "Material", cLVLProfile.equipmentId);
            return View(cLVLProfile);
        }

        // POST: Administrator/AdminSite/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("cID,cFirstName,cLastName,cEmail,cPhone,Img,DOB,Sex,Address,City,Country,JDate,Salary,Password,IBan,DepartmentName,positionName,equipmentId")] CLVLProfile cLVLProfile)
        {
            if (id != cLVLProfile.cID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cLVLProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CLVLProfileExists(cLVLProfile.cID))
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
            ViewData["equipmentId"] = new SelectList(_context.equipment, "equipmentId", "Material", cLVLProfile.equipmentId);
            return View(cLVLProfile);
        }

        // GET: Administrator/AdminSite/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.C_Level == null)
            {
                return NotFound();
            }

            var cLVLProfile = await _context.C_Level
                .Include(c => c.equipment)
                .FirstOrDefaultAsync(m => m.cID == id);
            if (cLVLProfile == null)
            {
                return NotFound();
            }

            return View(cLVLProfile);
        }

        // POST: Administrator/AdminSite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.C_Level == null)
            {
                return Problem("Entity set 'AppDbContext.C_Level'  is null.");
            }
            var cLVLProfile = await _context.C_Level.FindAsync(id);
            if (cLVLProfile != null)
            {
                _context.C_Level.Remove(cLVLProfile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CLVLProfileExists(Guid id)
        {
          return (_context.C_Level?.Any(e => e.cID == id)).GetValueOrDefault();
        }
    }
}
