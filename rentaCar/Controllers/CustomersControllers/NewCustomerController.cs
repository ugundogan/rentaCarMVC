using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rentaCar.Models.Entities;

namespace rentaCar.Controllers.CustomersControllers
{
    public class NewCustomerController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();

        // GET: NewCustomer
        [HttpGet]
        public ActionResult NewCustomer()
        {
            return View();
        }
        [HttpPost]

        public ActionResult NewCustomer(customers customer)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCustomer");
            }
            db.customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("../Customers/index");
        }
    }
}