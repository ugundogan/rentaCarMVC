using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers.CarsControllers
{
    public class AvailableCarsController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: AvailableCars
        public ActionResult AvailableCars(dates date)
        {
            var carData = db.cars.ToList();

            var rentData = db.rent.ToList();

            //DateTime startdate = DateTime.Parse("2023-08-16 10:48:00.000");
            //DateTime finishdate = DateTime.Parse("2023-08-10 10:48:00.000");

            //var query = from c in db.cars
            //            join r in db.rent on c.Id equals r.CarId
            //            where
            //            (
            //                !(startdate >= r.RentalDate && startdate <= r.ReturnDate) &&
            //                !(finishdate >= r.RentalDate && finishdate <= r.ReturnDate)
            //            )
            //            select new
            //            {
            //                Car = c,
            //                Rental = r
            //            };

            //var result = query.ToList();



            //var result = from c in carData
            //             join r in rentData on c.Id equals r.CarId 
            //             where
            //            (
            //                !(startdate >= r.RentalDate && startdate <= r.ReturnDate) &&
            //                !(finishdate >= r.RentalDate && finishdate <= r.ReturnDate)
            //            )
            //             select new
            //             {
            //                 CarData = c,
            //                 RentData = r
            //             };

            DateTime startDate = date.d1;
            DateTime finishDate = date.d2;

            //var values = from r in rentData
            //             join c in carData on r.CarId equals c.Id into carGroup
            //             from c in carGroup.DefaultIfEmpty()
            //             where
            //             (
            //                 !(startdate >= r.RentalDate && startdate <= r.ReturnDate) &&
            //                 !(finishdate >= r.RentalDate && finishdate <= r.ReturnDate)
            //             )
            //             select new
            //             {
            //                 c.Id,
            //                 c.LicensePlate,
            //                 c.Brand,
            //                 c.Model,
            //                 c.ProductYear,
            //                 c.Color,
            //                 c.km,
            //                 c.CarType,
            //                 c.DailyPrice,
            //             };
            //var availableCars = values.Concat(
            //            from c in carData
            //            join r in rentData on c.Id equals r.CarId into rentGroup
            //            from r in rentGroup.DefaultIfEmpty()
            //            where
            //            (
            //                c.RentState == 1 && r == null
            //            )
            //            select new
            //            {
            //                c.Id,
            //                c.LicensePlate,
            //                c.Brand,
            //                c.Model,
            //                c.ProductYear,
            //                c.Color,
            //                c.km,
            //                c.CarType,
            //                c.DailyPrice,
            //            }
            //        );

            //var result = availableCars.ToList();
            var rentedCarIdsQuery =
                from c in db.cars
                join r in db.rent on c.Id equals r.CarId into rentedCars
                where rentedCars.Any(r =>
                    (startDate >= r.RentalDate && startDate <= r.ReturnDate) ||
                    (finishDate >= r.RentalDate && finishDate <= r.ReturnDate))
                select c.Id;
            var xx = rentedCarIdsQuery.ToList();

            var query =
                from c in db.cars
                where !rentedCarIdsQuery.Contains(c.Id) ||
                      !db.rent.Any(r => r.CarId == c.Id && c.RentState == 1)
                select c;

            var availableCars = query.ToList();


            ViewBag.values = availableCars;

            return View();
        }


        [HttpGet]
        public ActionResult getAvailableCars(dates ajaxData)
        {
            var carData = db.cars.ToList();

            var rentData = db.rent.ToList();
            DateTime startDate = ajaxData.d1;
            DateTime finishDate = ajaxData.d2;

            var rentedCarIdsQuery =
               from c in db.cars
               join r in db.rent on c.Id equals r.CarId into rentedCars
               where rentedCars.Any(r =>
                   (startDate >= r.RentalDate && startDate <= r.ReturnDate) ||
                   (finishDate >= r.RentalDate && finishDate <= r.ReturnDate))
               select c.Id;

            var query =
                from c in db.cars
                where !rentedCarIdsQuery.Contains(c.Id) ||
                      (c.RentState == 1 && !db.rent.Any(r => r.CarId == c.Id))
                select new
                {
                    c.Id,
                    c.LicensePlate,
                    c.Brand,
                    c.Model,
                    c.ProductYear,
                    c.Color,
                    c.km,
                    c.CarType,
                    c.DailyPrice,
                };

            var availableCars = query.ToList();
            ViewBag.values = availableCars.ToList();

            var jsonValues = JsonConvert.SerializeObject(query);

            return Json(jsonValues, JsonRequestBehavior.AllowGet);
            //return View();

        }
    }
}