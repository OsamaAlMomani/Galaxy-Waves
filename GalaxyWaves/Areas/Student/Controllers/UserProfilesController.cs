using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galaxy.Core.Models.Student_Profile;
using GalaxyWaves.Data;

namespace GalaxyWaves.Areas.Student.Controllers
{
    [Area("Student")]
    public class UserProfilesController : Controller
    {
        private readonly AppDbContext _context;

        public UserProfilesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Student/UserProfiles
        public async Task<IActionResult> Index()
        {
              return _context.UserProfile != null ? 
                          View(await _context.UserProfile.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.UserProfile'  is null.");
        }

        // GET: Student/UserProfiles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UserProfile == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // GET: Student/UserProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/UserProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Full_Name,cEmail,Phone,Img,DOB,Sex,Address,City,Country,JDate,Password,CreditCard,CVV,CardEXP,CardName")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                userProfile.UserId = Guid.NewGuid();
                _context.Add(userProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userProfile);
        }

        // GET: Student/UserProfiles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UserProfile == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return View(userProfile);
        }

        // POST: Student/UserProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,Full_Name,cEmail,Phone,Img,DOB,Sex,Address,City,Country,JDate,Password,CreditCard,CVV,CardEXP,CardName")] UserProfile userProfile)
        {
            if (id != userProfile.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.UserId))
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
            return View(userProfile);
        }

        // GET: Student/UserProfiles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UserProfile == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: Student/UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserProfile == null)
            {
                return Problem("Entity set 'AppDbContext.UserProfile'  is null.");
            }
            var userProfile = await _context.UserProfile.FindAsync(id);
            if (userProfile != null)
            {
                _context.UserProfile.Remove(userProfile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfileExists(Guid id)
        {
          return (_context.UserProfile?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
