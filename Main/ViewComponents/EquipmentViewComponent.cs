﻿using Main.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Main.ViewComponents
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
