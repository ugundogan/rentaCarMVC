using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers
{
    public class CustomersController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: Customers
        public ActionResult Index()
        {
            
            var values = db.customers.ToList();
            ViewBag.Customers = values;
            return View();
        }


        public ActionResult DeleteCustomer(int id)
        {
            var customer = db.customers.Find(id);
            db.customers.Remove(customer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditCustomer(customers customer)
        {
            var values = db.customers.Find(customer.Id);
            values.IdentitiyNumber = customer.IdentitiyNumber;
            values.FullName = customer.FullName;
            values.Phone = customer.Phone;
            values.Adress = customer.Adress;
            values.Mail = customer.Mail;
            values.DriverLicenseNo = customer.DriverLicenseNo;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}