using rentaCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace rentaCar.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        rentaCarEntities db = new rentaCarEntities();
        [HttpGet]
        public ActionResult CreateUser()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(Users user)
        {
            var preBase64 = user.UserPassword;
            var messageBytes = Encoding.UTF8.GetBytes(preBase64);
            var encodedPassword = Convert.ToBase64String(messageBytes);
            user.UserPassword = encodedPassword;
            db.Users.Add(user);
            db.SaveChanges();
            return View();
        }
    }
}