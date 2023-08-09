using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rentaCar.Models.Entities;
namespace rentaCar.Controllers.CarsControllers
{
    public class GetCarController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: GetCar
        public ActionResult GetCar(int id)
        {
            var values = db.cars.Find(id);
            return View("GetCar", values);
        }
    }
}