using Main.DataAccess;
using Main.Models.AdminModels.ViewModel;
using Main.Models.AdminModels;
using Main.Models.TeacherModels;
using Main.Models.TeacherModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Main.Areas.Tutor.Controllers
{
    [Area("Tutor")]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CoursesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Tutor/Courses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.course.Include(c => c.Core);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Tutor/Courses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.course == null)
            {
                return NotFound();
            }

            var course = await _context.course
                .Include(c => c.Core)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Tutor/Courses/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName");
            return View();
        }

        // POST: Tutor/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                course.CourseId = Guid.NewGuid();
                string img = uploadImg(course);
                string video = uploadVideo(course);
                var music = new Course
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    CourseDescription = course.CourseDescription,
                    CourseImg = img,
                    CourseVideo = video,
                    CoreId = course.CoreId,
                    Price = course.Price,
                };
                _context.Add(music);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName", course.CoreId);
            return View(course);
        }

        // GET: Tutor/Courses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.course == null)
            {
                return NotFound();
            }

            var course = await _context.course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            CourseViewModel course_ = new CourseViewModel
            {
                CourseName = course.CourseName,
                CourseId = course.CourseId,
                CourseDescription = course.CourseDescription,
            };
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName", course.CoreId);
            return View(course_);
        }

        // POST: Tutor/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CourseViewModel course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
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
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName", course.CoreId);
            return View(course);
        }

        // GET: Tutor/Courses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.course == null)
            {
                return NotFound();
            }

            var course = await _context.course
                .Include(c => c.Core)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Tutor/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.course == null)
            {
                return Problem("Entity set 'AppDbContext.course'  is null.");
            }
            var course = await _context.course.FindAsync(id);
            if (course != null)
            {
                _context.course.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(Guid id)
        {
          return (_context.course?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }

        public string uploadImg(CourseViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.CourseImg!.FileName);
            string newImgName = "GalaxyWaves_" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.CourseImg.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.CourseImg.CopyTo(fileStream);
            }
            return "\\Images\\" + newImgName;
        }

        public string uploadVideo(CourseViewModel model)
        {
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { }
            string contentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(contentPath)) { }
            string p = Path.Combine(wwwPath, "CourseVideo");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(model.CourseVideo!.FileName);
            string newImgName = "GalaxyWaves_" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.CourseVideo.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.CourseVideo.CopyTo(fileStream);
            }
            return "\\CourseVideo\\" + newImgName;
        }
    }
}
