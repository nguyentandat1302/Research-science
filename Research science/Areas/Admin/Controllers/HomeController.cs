using Newtonsoft.Json;
using Research_science.Areas.Navigation;
using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_science.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
      

        Model1 db = new Model1();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string UserName = f["UserName"];
            string Password = f["Password"];
            var admin = db.Admins.FirstOrDefault(x => x.UserName == UserName && x.Password == Password);
            if (admin != null)
            {
                Session["Admin"] = admin;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "UserName or Password do not exists";
            }
            return View();
        }
        public ActionResult MenuPartial()
        {
            var path = Server.MapPath("~/Areas/Navigation/Navigation.json");
            var content = System.IO.File.ReadAllText(path);
            var menu = JsonConvert.DeserializeObject<NavigationMenu>(content);
            return PartialView(menu);
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Index");
        }
    }
}