using Newtonsoft.Json;
using rentaCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rentaCar.Controllers
{
    public class CalendarController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();

        // GET: Calendar
        public JsonResult GetRent()
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

            var rents = db.rent.ToList();

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(rents, Formatting.Indented, jss);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}