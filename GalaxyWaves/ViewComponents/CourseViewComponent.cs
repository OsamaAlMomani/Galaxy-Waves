using GalaxyWaves.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyWaves.ViewComponents
{
    public class CourseViewComponent :ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public CourseViewComponent(ApplicationDbContext db)
        {
            
            _db = db;

        }
        public IViewComponentResult Invoke()
        {
            var data = _db.Course.OrderByDescending(x => x.CourseName).Take(12);
            return View(data);
        }
    }

}
