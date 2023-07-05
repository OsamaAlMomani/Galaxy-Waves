using GalaxyWaves_UI.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyWaves_UI.ViewComponents
{
    public class EquipmentViewComponent : ViewComponent
    {
        private AppDbContext db;
        public EquipmentViewComponent(AppDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.musicEquipment.OrderByDescending(x => x.Id);
            return View(data);
        }


    }
}
