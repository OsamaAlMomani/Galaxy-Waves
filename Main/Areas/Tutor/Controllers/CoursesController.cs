using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Main.DataAccess;
using Main.Models.TeacherModels;
using Main.Models.TeacherModels.ViewModel;

namespace Main.Areas.Tutor.Controllers
{
    [Area("Tutor")]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CoursesController(AppDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Tutor/Courses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.course.Include(c => c.Core).Include(c => c.teacher);
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
                .Include(c => c.teacher)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Tutor/Courses/Create
        public IActionResult Create()
        {
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName");
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "Id", "About");
            return View();
        }

        // POST: Tutor/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel Model)
        {
            if (ModelState.IsValid)
            {
                string videoMap = UploadVideo(Model);
                string ImgMap = UploadImg(Model);
                Model.CourseId = Guid.NewGuid();
                Model.TeacherID = Guid.NewGuid();
                var course = new Course
                {
                     CoreId = Model.CoreId,
                     CourseId = Model.CourseId,
                     CourseName = Model.CourseName,
                     CourseDescription = Model.CourseDescription,
                     CourseVideo = videoMap,
                     Price = Model.Price,
                     TeacherID = Model.TeacherID,
                       CourseImg= ImgMap,
                };
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName", Model.CoreId);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "Id", "About", Model.TeacherID);
            return View(Model);
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
            ViewData["CoreId"] = new SelectList(_context.cores, "CoreId", "CoreName", course.CoreId);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "Id", "About", course.TeacherID);
            return View(course);
        }

        // POST: Tutor/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,CourseViewModel course)
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
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "Id", "About", course.TeacherID);
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
                .Include(c => c.teacher)
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
        
        public string UploadVideo(CourseViewModel course)
        {
            if (!CourseExists(course.CourseId)) { }
            if (course == null){ Console.WriteLine("Error : Video object is null"); }
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { Console.WriteLine("Null WWW Path"); }
            string ContentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(ContentPath)) { Console.WriteLine("Null Content Path"); }
            string p = Path.Combine(wwwPath, "Galaxy_Video");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(course.CourseVideo!.FileName);
            string newVideoName = "GalaxyWaves_" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(course.CourseVideo.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newVideoName), FileMode.Create))
            {
                course.CourseVideo.CopyTo(fileStream);
            }
            return "\\Galaxy_Video\\" + newVideoName;
        }        
        public string UploadImg(CourseViewModel course)
        {
            if (!CourseExists(course.CourseId)) { }
            if (course == null){ Console.WriteLine("Error : Img object is null"); }
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { Console.WriteLine("Null WWW Path"); }
            string ContentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(ContentPath)) { Console.WriteLine("Null Content Path"); }
            string p = Path.Combine(wwwPath, "Galaxy_img");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(course.CourseVideo!.FileName);
            string newVideoName = "GalaxyWaves_" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(course.CourseVideo.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newVideoName), FileMode.Create))
            {
                course.CourseVideo.CopyTo(fileStream);
            }
            return "\\Galaxy_img\\" + newVideoName;
        }
    }
}
