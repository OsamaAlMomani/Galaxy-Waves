using GalaxyWave.SQL_Model.Models.UsersDashboards.Elements;
using GalaxyWave.UI.DataCenter;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyWave.UI.Areas.Layer1.Controllers
{
    public class Dashboard_First_LayerController : Controller
    {
        private readonly appDb db;
        public Dashboard_First_LayerController(appDb _db)
        {
            db= _db;
        }
        /// <summary>
        ///         passing user Id to return users dashboard, then 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult Index(Guid Id)
        {
            return View();
        }
    }
}
