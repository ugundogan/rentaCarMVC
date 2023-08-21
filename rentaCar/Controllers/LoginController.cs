using rentaCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace rentaCar.Controllers
{
    [AllowAnonymous]
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
            if(!ModelState.IsValid)
            {
                return View("Index");
            }
            var preBase64 = user.UserPassword;
            var encodedBytes = Encoding.UTF8.GetBytes(preBase64);
            var password = Convert.ToBase64String(encodedBytes);
            //var decodedBytes = Convert.FromBase64String(user.UserPassword);
            //var originalPassword = Encoding.UTF8.GetString(decodedBytes);
            var userinfo = db.Users.FirstOrDefault(x=>x.UserName == user.UserName && x.UserPassword == password);
            if (userinfo != null)
            {
                FormsAuthentication.SetAuthCookie(userinfo.UserName, false);
                Session["UserName"] = userinfo.UserName;
                return RedirectToAction("Index", "rents");
            }
            else
            {
                ViewBag.message = "Kullanıcı Adı veya Şifre Hatalı";
                return View("Index");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); 
            return RedirectToAction("Index");
        }
    }
}