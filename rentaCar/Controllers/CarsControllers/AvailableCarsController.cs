using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers.CarsControllers
{
    public class AvailableCarsController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: AvailableCars
        public ActionResult AvailableCars()
        {
            var carData = db.cars.ToList();

            var rentData = db.rent.ToList();

            var result = from c in carData
                         join r in rentData on c.Id equals r.CarId into gj
                         from subrent in gj.DefaultIfEmpty()
                         select new
                         {
                             CarData = c,
                             RentData = subrent
                         };

            ViewBag.values = rentData.ToList();
            return View();
        }
    }
}