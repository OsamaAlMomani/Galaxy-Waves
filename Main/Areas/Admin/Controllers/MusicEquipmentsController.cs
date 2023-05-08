using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Main.Areas.Admin.Models;
using Main.DataAccess;
using Main.Areas.Admin.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MusicEquipmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MusicEquipmentsController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/MusicEquipments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.musicEquipment.Include(m => m.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/MusicEquipments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.musicEquipment == null)
            {
                return NotFound();
            }

            var musicEquipment = await _context.musicEquipment
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicEquipment == null)
            {
                return NotFound();
            }

            return View(musicEquipment);
        }

        // GET: Admin/MusicEquipments/Create
        public IActionResult Create()
        {
            ViewData["categoryName"] = new SelectList(_context.categories, "CategoryId", "categoryName");
            return View();
        }

        // POST: Admin/MusicEquipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MusicEquipmentViewModel musicEquipment)
        {
            if (ModelState.IsValid)
            {
                string imgPath = uploadImg(musicEquipment);
                musicEquipment.Id = Guid.NewGuid();

                var music = new MusicEquipment
                {
                    Id = musicEquipment.Id,
                    Name = musicEquipment.Name,
                    About_It = musicEquipment.About_It,
                    Img= imgPath,
                    categoryName=musicEquipment.categoryName,                  
                };

                _context.musicEquipment.Add(music);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoryName"] = new SelectList(_context.categories, "CategoryId", "categoryName", musicEquipment.categoryName);
            return View(musicEquipment);
        }

        // GET: Admin/MusicEquipments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.musicEquipment == null)
            {
                return NotFound();
            }

            var musicEquipment = await _context.musicEquipment.FindAsync(id);
            if (musicEquipment == null)
            {
                return NotFound();
            }
            ViewData["categoryName"] = new SelectList(_context.categories, "CategoryId", "categoryName", musicEquipment.categoryName);
            return View(musicEquipment);
        }

        // POST: Admin/MusicEquipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,categoryName,Img,About_It")] MusicEquipment musicEquipment)
        {
            if (id != musicEquipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicEquipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicEquipmentExists(musicEquipment.Id))
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
            ViewData["categoryName"] = new SelectList(_context.categories, "CategoryId", "categoryName", musicEquipment.categoryName);
            return View(musicEquipment);
        }

        // GET: Admin/MusicEquipments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.musicEquipment == null)
            {
                return NotFound();
            }

            var musicEquipment = await _context.musicEquipment
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicEquipment == null)
            {
                return NotFound();
            }

            return View(musicEquipment);
        }

        // POST: Admin/MusicEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.musicEquipment == null)
            {
                return Problem("Entity set 'AppDbContext.musicEquipment'  is null.");
            }
            var musicEquipment = await _context.musicEquipment.FindAsync(id);
            if (musicEquipment != null)
            {
                _context.musicEquipment.Remove(musicEquipment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicEquipmentExists(Guid id)
        {
          return (_context.musicEquipment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public string uploadImg(MusicEquipmentViewModel model)
        {
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { }
            string contentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(contentPath)) { }
            string p = Path.Combine(wwwPath, "Images");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(model.Img!.FileName);
            string newImgName = "nextwo_" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.Img.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Img.CopyTo(fileStream);
            }
            return "\\Images\\" + newImgName;
        }


    }
}
