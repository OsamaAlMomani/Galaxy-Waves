using Main.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Dynamic;

namespace Main.Areas.Teacher.Controllers
{
    [Area("Teacher")]

    public class TeacherDashboardController : Controller
    {
        private readonly AppDbContext db;
        public TeacherDashboardController (AppDbContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public async Task<IActionResult> teacher_Dashboard(Guid TId)
        {
            if (TId == null)
            {
                return NotFound();
            }
            var result = await db.teacherProfiles.Include(x => x.course).ToListAsync();

            var Teacher_P = result.FirstOrDefault(x=>x.TeacherProfile_Id == TId);
            if (result==null)
            {
                return NotFound();
            }
            return View(Teacher_P);
        }

    }
}
