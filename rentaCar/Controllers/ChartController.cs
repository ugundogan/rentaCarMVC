using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers
{
    public class ChartController : Controller
    {
        // GET: PieChar
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PieChart()
        {
            rentaCarEntities db = new rentaCarEntities();
            var car = db.cars.ToList();


            return Json(car, JsonRequestBehavior.AllowGet);
        }
    }
}