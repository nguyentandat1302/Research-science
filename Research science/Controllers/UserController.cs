using Research_science.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

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
        // Company 
        [HttpPost]
        public ActionResult SignUp(Users model, HttpPostedFileBase logoFile)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên người dùng đã tồn tại chưa
                var existingUser = db.Users.FirstOrDefault(u => u.UserName == model.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "This username is already taken");
                    return View(model);
                }

                if (logoFile != null && logoFile.ContentLength > 0)
                {
                    if (logoFile.ContentType != "image/jpeg" && logoFile.ContentType != "image/png")
                    {
                        ModelState.AddModelError("LogoCompany", "Only JPG and PNG formats are supported for logo");
                        return View(model);
                    }

                    // Kiểm tra kích thước tập tin (giả sử tối đa là 5MB)
                    if (logoFile.ContentLength > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("LogoCompany", "Logo file size cannot exceed 5MB");
                        return View(model);
                    }

                    model.LogoCompany = Utility.ConvertImageToBase64(Image.FromStream(logoFile.InputStream, true, true));
                }
                model.MaLoaiUser = 1;
                db.Users.Add(model);
                db.SaveChanges();

                return RedirectToAction("Login", "User");
            }

            return View(model);
        }


        //Freelancer
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem có người dùng nào đã sử dụng email này chưa
                var existingUser = db.Users.FirstOrDefault(u => u.UserName == model.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "This email address is already registered");
                    return View(model); // Trả về view với thông báo lỗi
                }

                // Chú ý: Ngày tháng đã được lưu trong thuộc tính BirthDay của model Users
                model.MaLoaiUser = 2;
                db.Users.Add(model);
                db.SaveChanges();

                return RedirectToAction("Login", "User");
            }

            return View(model);
        }




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Users.FirstOrDefault(k => (k.UserName == user.UserName) && k.Password == user.Password);

                if (customer != null)
                {
                    if (customer.MaLoaiUser == 1)
                    {
                        //Em dau co tao Area Employer dau, khi tao earea ma moi kb nhe
                        Session["UserName"] = customer;
                        return RedirectToAction("Menu", "Employer"); // Chỉ định khu vực là "Employer"
                    }
                    else if (customer.MaLoaiUser == 2)
                    {
                        Session["UserName"] = customer;
                        return RedirectToAction("Index", "HomePage");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại hoặc mật khẩu không đúng.");
                }
            }

            return View(user);
        }









        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return Redirect("~/");
        }

       




    }
}