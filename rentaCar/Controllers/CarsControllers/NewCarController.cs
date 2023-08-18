using System;
using System.Collections.Generic;
using System.IO;
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
        //[HttpGet]
        //public ActionResult NewCar()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult NewCar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCar(cars car)
        {   if(car.ImageFile == null)
            {
                car.Image = "Null";
            }
            if (car.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(car.ImageFile.FileName);
                string extension = Path.GetExtension(car.ImageFile.FileName);
                fileName = fileName + extension;
                car.Image = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                car.ImageFile.SaveAs(fileName);
            }
            car.RentState = 1;
            db.cars.Add(car);
            db.SaveChanges();


            return RedirectToAction("../cars/index");
        }
    }
}