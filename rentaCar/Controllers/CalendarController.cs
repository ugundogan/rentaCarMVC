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
            var rents = db.rent.ToList();

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(rents, Formatting.Indented, jss);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}