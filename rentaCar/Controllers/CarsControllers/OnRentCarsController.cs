using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers.CarsControllers
{
    public class OnRentCarsController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: OnRentCars
        public ActionResult OnRentCars()
        {
            var values = db.cars.ToList();
            return View(values);
        }
    }
}