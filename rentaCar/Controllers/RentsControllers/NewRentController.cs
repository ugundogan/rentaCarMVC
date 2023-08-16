using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers.RentsControllers
{
    public class NewRentController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: NewRent

        [HttpGet]

        public ActionResult NewRent()
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

            var values = db.cars.ToList();
            ViewBag.values = values;
            return View();
        }

        [HttpPost]

        public ActionResult NewRent(rent rent)
        {
            var car = db.cars.Where(m => m.Id == rent.cars.Id).FirstOrDefault();
            rent.cars = car;
            var customer = db.customers.Where(m => m.Id == rent.customers.Id).FirstOrDefault();
            rent.customers = customer;

            db.rent.Add(rent);
            db.SaveChanges();

            return RedirectToAction("../Rents/Index");
        }
    }
}