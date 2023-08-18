using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers
{
    public class CarsController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: Cars
        public ActionResult Index()
        {
            var values = db.cars.ToList();
            ViewBag.Values = values;
            return View(values);
        }

        public ActionResult DeleteCar(int id)
        {
            var car = db.cars.Find(id);
            db.cars.Remove(car);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult EditCar(cars car)
        {
            var values = db.cars.Find(car.Id);
            if (car.ImageFile != null)
            {
                if (System.IO.File.Exists(Server.MapPath(car.Image)))
                {
                    System.IO.File.Delete(Server.MapPath(car.Image));
                }
                WebImage img = new WebImage(car.ImageFile.InputStream);
                FileInfo imginfo = new FileInfo(car.ImageFile.FileName);
                string filename = car.ImageFile.FileName;
                img.Save("~/Image/" + filename);

                values.Image = "~/Image/" + filename;
            }
            
            values.LicensePlate = car.LicensePlate;
            values.Brand = car.Brand;
            values.Model = car.Model;
            values.ProductYear = car.ProductYear;
            values.Color = car.Color;
            values.km = car.km;
            values.CarType = car.CarType;
            values.RentState = car.RentState;
            values.DailyPrice = car.DailyPrice;
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        [ValidateInput(false)]
        public ActionResult DeleteImage(int id) {
            var values = db.cars.Find(id);
            
            if(!string.IsNullOrEmpty(values.Image))
            {
                if (System.IO.File.Exists(Server.MapPath(values.Image)))
                {
                    System.IO.File.Delete(Server.MapPath(values.Image));
                }
                values.Image = "~/Image/no-image.png";
                db.SaveChanges();
                return RedirectToAction("Getcar/"+ values.Id,"GetCar");
            }

                return RedirectToAction("Getcar/" + values.Id,"GetCar");
        }

    }
}