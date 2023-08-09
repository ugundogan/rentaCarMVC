using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rentaCar.Models.Entities;
namespace rentaCar.Controllers.CustomersControllers
{
    public class GetCustomerController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: GetCustomer
        public ActionResult GetCustomer(int id)
        {
            var values = db.customers.Find(id);
            return View("GetCustomer", values);
        }
    }
}