using rentaCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace rentaCar.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public string SHA256Hash(string text)
        {
            string source = text;
            using (SHA256 sha1Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return (hash);
            }
        }
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
            var password = SHA256Hash(user.UserPassword);
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