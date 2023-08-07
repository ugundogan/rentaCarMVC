using System;
using System.Collections.Generic;
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
            return View(values);
        }

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


            return RedirectToAction("Index");
        }

        public ActionResult DeleteCar(int id)
        {
            var car = db.cars.Find(id);
            db.cars.Remove(car);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        

        public ActionResult OnRentCars()
        {
            var values = db.cars.ToList();
            return View(values);
        }

        public ActionResult AvailableCars()
        {
            var values = db.cars.ToList();
            return View(values);
        }

        public ActionResult GetCar(int id)
        {
            var values = db.cars.Find(id);
            return View("GetCar",values);
        }

        public ActionResult EditCar(cars car)
        {
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
            db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}