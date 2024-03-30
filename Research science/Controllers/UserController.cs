using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_science.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Users Model)
        {
            if (ModelState.IsValid)
            {
                var kh = db.Users.FirstOrDefault(k => k.UserName == Model.UserName);
                if (kh != null)
                {
                    ModelState.AddModelError("UserName", "This account has already existed");
                    return View(Model);
                }

                db.Users.Add(Model);
                db.SaveChanges();

             
                return RedirectToAction("Login", "User");
            }

            return View(Model);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Users Model)
        {
            if (ModelState.IsValid)
            {
                var kh = db.Users.FirstOrDefault(k => k.UserName == Model.UserName);
                if (kh != null)
                {
                    ModelState.AddModelError("UserName", "This account has already existed");
                    return View(Model);
                }

                db.Users.Add(Model);
                db.SaveChanges();

                return RedirectToAction("Login", "User");
            }

            return View(Model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Users.FirstOrDefault(k => k.UserName == user.UserName && k.Password == user.Password);
                var employer = db.Users.FirstOrDefault(k => k.UserName == user.UserName && k.Password == user.Password);

                //if (customer != null)
                //{
                //    // Đăng nhập là khách hàng
                //    Session["IDCustomer"] = customer.IDCustomer;
                //    Session["UserName"] = customer.UserName;
                //    Session["Customer"] = customer;
                //    return RedirectToAction("Index", "HomePage");
                //}
                //else if (employer != null)
                //{
                //    // Đăng nhập là nhà tuyển dụng
                //    Session["Employer"] = employer;
                //    return RedirectToAction("Menu", "Employer", new { Area = "Employer" });
                //}
                //else
                //{
                //    ModelState.AddModelError("Password", "Tài khoản không tồn tại hoặc mật khẩu không đúng.");
                //}
            }

            return View();
        }



        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return Redirect("~/");
        }

       




    }
}