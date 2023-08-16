using rentaCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rentaCar.Controllers
{
    public class RentsController : Controller
    {
        // GET: Rents
        rentaCarEntities db = new rentaCarEntities();
        public ActionResult Index()
        {
            var values = db.rent.ToList();

            return View(values);



        }

        //public ActionResult Details(int id) { }
        public ActionResult GetRent(int id)
        {
            var values = db.rent.Find(id);
            return View("GetRent", values);
        }
        public ActionResult EditRent(rent rent) 
        {
            var values = db.rent.Find(rent.Id);
            values.CustomerId = rent.CustomerId;
            values.CarId = rent.CarId;
            values.RentalDate = rent.RentalDate;
            values.ReturnDate = rent.ReturnDate;
            values.Note = rent.Note;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeleteRent(int id)
        {
            var rent = db.rent.Find(id);
            db.rent.Remove(rent);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

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

            return RedirectToAction("Index");
        }

    }
}