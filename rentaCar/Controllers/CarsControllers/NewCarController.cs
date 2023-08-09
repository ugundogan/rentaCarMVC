using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers
{
    public class NewCarController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: NewCar
        [HttpGet]
        public ActionResult NewCar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult NewCar(cars car)
        {
            db.cars.Add(car);
            db.SaveChanges();


            return View();
        }
    }
}