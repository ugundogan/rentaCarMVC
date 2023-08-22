using rentaCar.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace rentaCar.Controllers
{
    public class UserController : Controller
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
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            string pass = SHA256Hash(user.UserPassword);
            user.UserPassword = pass;
            db.Users.Add(user);
            db.SaveChanges();
            return View();
        }
    }
}