﻿using Research_science.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult SignUp(Employer Model)
        {
            if (ModelState.IsValid)
            {
                var kh = db.Employer.FirstOrDefault(k => k.UserName == Model.UserName);
                if (kh != null)
                {
                    ModelState.AddModelError("UserName", "This account has already existed");
                    return View(Model);
                }

                db.Employer.Add(Model);
                db.SaveChanges();
                return View("Login");

            }
            return View(Model);
        }
        [HttpGet]
        public ActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer Model)
        {
            if (ModelState.IsValid)
            {
                var kh = db.Customer.FirstOrDefault(k => k.UserName == Model.UserName);
                if (kh != null)
                {
                    ModelState.AddModelError("UserName", "This account has already existed");
                    return View(Model);
                }

                db.Customer.Add(Model);
                db.SaveChanges();
                return View("Login");

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
                var customer = db.Customer.FirstOrDefault(k => k.UserName == user.UserName && k.Password == user.Password);
                var employer = db.Employer.FirstOrDefault(k => k.UserName == user.UserName && k.Password == user.Password);

                if (customer != null)
                {
                    // Đăng nhập là khách hàng
                    Session["IDCustomer"] = customer.IDCustomer;
                    Session["UserName"] = customer.UserName;
                    Session["Customer"] = customer;
                    return RedirectToAction("Navbar", "HomePage");
                }
                else if (employer != null)
                {
                    // Đăng nhập là nhà tuyển dụng
                    Session["Employer"] = employer;
                    return RedirectToAction("Navbar", "HomePage", new { Area = "Employer" });
                }
                else
                {
                    ModelState.AddModelError("Password", "Tài khoản không tồn tại hoặc mật khẩu không đúng.");
                }
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