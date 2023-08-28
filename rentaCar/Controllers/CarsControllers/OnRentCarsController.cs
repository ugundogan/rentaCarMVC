using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers.CarsControllers
{
    public class OnRentCarsController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: OnRentCars
        public ActionResult OnRentCars()
        {
            List<SelectListItem> valuesCar = (from i in db.cars.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.LicensePlate,
                                                  Value = i.Id.ToString(),
                                              }).ToList();
            ViewBag.valueCar = valuesCar;
            List<SelectListItem> valuesCustomer = (from i in db.customers.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.FullName,
                                                       Value = i.Id.ToString(),
                                                   }).ToList();
            ViewBag.valueCustomer = valuesCustomer;

            //var values = db.cars.ToList();
            //ViewBag.values = values;
            var values = db.cars.ToList();
            var aa = from c in db.cars
                     where (c.RentState == 1)
                     select c;
            var a = aa.Count();
            var bb = from c in db.cars
                     where (c.RentState == 0)
                     select c;

            var b = bb.Count();

            var rentedCarIdsQuery =
                from c in db.cars
                join r in db.rent on c.Id equals r.CarId into rentedCars
                where rentedCars.Any(r => r.CarId == c.Id)
                select c;
            var w = rentedCarIdsQuery.Count();

            var query =
                from c in db.cars
                where !rentedCarIdsQuery.Contains(c) ||
                      (c.RentState == 1 && !db.rent.Any(r => r.CarId == c.Id))
                select c;

            var y = query.Count();

            ViewBag.a = a;
            ViewBag.b = b;
            ViewBag.c = w;
            ViewBag.d = a - y;
            return View();
        }
    }
}