using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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


        

        public ActionResult EditCar(cars car)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(car.ImageFile.FileName);
                string extension = Path.GetExtension(car.ImageFile.FileName);
                fileName = fileName + extension;
                car.Image = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                car.ImageFile.SaveAs(fileName);
            }
            var values = db.cars.Find(car.Id);
            values.LicensePlate = car.LicensePlate;
            values.Brand = car.Brand;
            values.Model = car.Model;
            values.ProductYear = car.ProductYear;
            values.Color = car.Color;
            values.km = car.km;
            values.CarType = car.CarType;
            values.RentState = car.RentState;
            values.DailyPrice = car.DailyPrice;
            values.Image = car.Image;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}