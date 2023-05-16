using Main.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Main.ViewComponents
{
    public class CourseViewComponent:ViewComponent
    {


        private AppDbContext db;
        public CourseViewComponent(AppDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.course.OrderByDescending(x => x.CourseId);
            return View(data);
        }
    }
}
