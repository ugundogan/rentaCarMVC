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
    public class AjaxData
    {
        public DateTime d1 { get; set; }
        public DateTime d2 { get; set; }
        public string a { get; set; }   
    }
    public class AvailableCarsController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: AvailableCars
        public ActionResult AvailableCars()
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

            DateTime startdate = DateTime.Parse("2023-08-16 10:48:00.000");
            DateTime finishdate = DateTime.Parse("2023-08-10 10:48:00.000");

            var query = from r in rentData
                        join c in carData on r.CarId equals c.Id into carGroup
                        from c in carGroup.DefaultIfEmpty()
                        where
                        (
                            !(startdate >= r.RentalDate && startdate <= r.ReturnDate) &&
                            !(finishdate >= r.RentalDate && finishdate <= r.ReturnDate)
                        )
                        select new
                        {
                            r.Id,
                            r.CarId,
                            r.CustomerId,
                            r.RentalDate,
                            r.ReturnDate,
                            r.Note
                        };

            var result = query.ToList();


            ViewBag.values = result;

            return View();
        }


        [HttpPost]
        public ActionResult getAvailableCars(AjaxData ajaxData)
        {
            var carData = db.cars.ToList();

            var rentData = db.rent.ToList();
            DateTime startdate = ajaxData.d1;
            DateTime finishdate = ajaxData.d2;

            var values = from r in rentData
                         join c in carData on r.CarId equals c.Id into carGroup
                         from c in carGroup.DefaultIfEmpty()
                         where
                         (
                             !(startdate >= r.RentalDate && startdate <= r.ReturnDate) &&
                             !(finishdate >= r.RentalDate && finishdate <= r.ReturnDate)
                         )
                         select new
                         {
                             r.Id,
                             r.CarId,
                             r.CustomerId,
                             r.RentalDate,
                             r.ReturnDate,
                             r.Note
                         };

            var jsonValues = JsonConvert.SerializeObject(values);

            return Json(jsonValues, JsonRequestBehavior.AllowGet);

        }
        public ActionResult getAvailableCar()
        {
            return View();
        }
    }
}