using rentaCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace rentaCar.Controllers
{
    public class LoginController : Controller
    {
        rentaCarEntities db = new rentaCarEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(Users user)
        {
            var preBase64 = user.UserPassword;
            var encodedBytes = Encoding.UTF8.GetBytes(preBase64);
            var password = Convert.ToBase64String(encodedBytes);
            //var decodedBytes = Convert.FromBase64String(user.UserPassword);
            //var originalPassword = Encoding.UTF8.GetString(decodedBytes);
            var userinfo = db.Users.FirstOrDefault(x=>x.UserName == user.UserName && x.UserPassword == password);
            if (userinfo != null)
            {
                return RedirectToAction("Index", "rents");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}